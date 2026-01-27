using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.Threading;

namespace DataParallelismWithForEach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() { InitializeComponent(); }

        private CancellationTokenSource _cancelToken = new();
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            _cancelToken.Cancel();
        }
        private void cmdProcess_Click(object sender, EventArgs e)
        {
            this.Title = $"Starting...";
            // ProcessFiles();
            // Try a Task.Factory
            Task.Factory.StartNew(() =>
            {
                ProcessFiles();
                Dispatcher?.Invoke(() => this.Title = "Processing Complete"); //update title when tasks are actually done. Otherwise 'done' gets set immediately
            }); //Action<T>
            //JoinableTaskFactory factory = new(new JoinableTaskContext());
            //factory.Run(async () => {
            //    await ProcessFilesAsync();
            //});
        }

        private async Task ProcessFilesAsync() {
            // Load up all *.jpg files, and make a new folder for the
            //   modified data. 
            //Get the directory path where the file is executing 
            //For VS 2022 debugging, the current directory will be <projectdirectory>\bin\debug\net6.0-windows 
            //For VS Code or “dotnet run”, the current directory will be <projectdirectory>
            var basePath = Directory.GetCurrentDirectory();
            var pictureDirectory = System.IO.Path.Combine(basePath, "TestPictures");
            var outputDirectory = System.IO.Path.Combine(basePath, "ModifiedPictures"); //Clear out any existing files
            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, true);
            }
            Directory.CreateDirectory(outputDirectory);
            string[] files = Directory.GetFiles(pictureDirectory, "*.jpg", SearchOption.AllDirectories); // Process the image data in a blocking manner.
            //Current code hangs on main thread. Parallel.ForEach still blocks on primary thread.
            // Support cancellation tokens
            ParallelOptions options = new();
            options.CancellationToken = this._cancelToken.Token;
            options.MaxDegreeOfParallelism = System.Environment.ProcessorCount; // extra parallelism over the processor count does not help performance. In fact, it makes
            // it more complex and often LESS performant.

            await Parallel.ForEachAsync(files, options, async (currentFile, token) =>
            {
                try
                {
                    string filename = System.IO.Path.GetFileName(currentFile); // Print out the ID of the thread processing the current image.
                                                                                // threads that try to access a control on main thread will throw exception
                                                                                // Try using anonymous delegate (WPF Dispatcher Invoke
                                                                                //this.Title = $"Processing {filename} on thread {Environment.CurrentManagedThreadId}";
                                                                                // throw if cancelled.
                    token.ThrowIfCancellationRequested();
                    Dispatcher?.Invoke(() => this.Title = $"Processing {filename}");
                    using Bitmap bitmap = new(currentFile);
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(System.IO.Path.Combine(outputDirectory, filename));
                    Thread.Sleep(2000);
                    Dispatcher?.Invoke(() => this.Title = "Done.");
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    if (ex is OperationCanceledException opCancelled) {
                        Dispatcher?.Invoke(() => this.Title = opCancelled.Message);
                    }
                }
                    
            });
        }
        private void ProcessFiles()
        {
            // Load up all *.jpg files, and make a new folder for the
            //   modified data. 
            //Get the directory path where the file is executing 
            //For VS 2022 debugging, the current directory will be <projectdirectory>\bin\debug\net6.0-windows 
            //For VS Code or “dotnet run”, the current directory will be <projectdirectory>
            var basePath = Directory.GetCurrentDirectory();
            var pictureDirectory = System.IO.Path.Combine(basePath, "TestPictures");
            var outputDirectory = System.IO.Path.Combine(basePath, "ModifiedPictures"); //Clear out any existing files
            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, true);
            }
            Directory.CreateDirectory(outputDirectory);
            string[] files = Directory.GetFiles(pictureDirectory, "*.jpg", SearchOption.AllDirectories); // Process the image data in a blocking manner.
            //Current code hangs on main thread. Parallel.ForEach still blocks on primary thread.
            // Support cancellation tokens
            ParallelOptions options = new();
            options.CancellationToken = this._cancelToken.Token;
            options.MaxDegreeOfParallelism = System.Environment.ProcessorCount; // extra parallelism over the processor count does not help performance. In fact, it makes
            // it more complex and often LESS performant.

            try {
                Parallel.ForEach(files, options, (currentFile) =>
                {
                    string filename = System.IO.Path.GetFileName(currentFile); // Print out the ID of the thread processing the current image.
                                                                               // threads that try to access a control on main thread will throw exception
                                                                               // Try using anonymous delegate (WPF Dispatcher Invoke
                                                                               //this.Title = $"Processing {filename} on thread {Environment.CurrentManagedThreadId}";
                    // throw if cancelled.
                    options.CancellationToken.ThrowIfCancellationRequested();
                    Dispatcher?.Invoke(() =>
                    {
                        this.Title = $"Processing {filename}";
                    });
                    using Bitmap bitmap = new(currentFile);
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(System.IO.Path.Combine(outputDirectory, filename));
                });
            } catch (OperationCanceledException ex) {
                Dispatcher?.Invoke(() => this.Title = ex.Message);
            }
            
        }
    }
}
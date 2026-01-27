using System.Text;
using System.Net;
using System.Diagnostics.Tracing;

char[] splitChars = [' ', '\u000A', ',', '.', ';', ':', '-', '?', '/'];
string _book = "";
GetBook();

Console.WriteLine("Downloading book...");
Console.ReadLine();

void GetBook()
{
    // obsolete, use HttpClient instead. Async / Await.
    using WebClient wc = new();
    wc.DownloadStringCompleted += (s, eArgs) =>
    {
        _book = eArgs.Result;
        Console.WriteLine("Download complete.");
        GetStats();
    };

    wc.DownloadStringAsync(new Uri("http://www.gutenberg.org/files/98/98-0.txt")); //this spins up a new thread from the ThreadPool automatically
}
void GetStats()
{
    string[] words = _book.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

    string[] topTenMostCommonWords = [];

    string longestWord = string.Empty;

    Parallel.Invoke(() => {
        topTenMostCommonWords = FindTenMostCommon(words);
    },
    () => {
        longestWord = FindLongestWord(words);
    });

    StringBuilder bookStats = new StringBuilder("Ten Most Common Words: \n");
    foreach (string s in topTenMostCommonWords) { 
        bookStats.AppendLine(s);
    }

    bookStats.AppendFormat("Longest word: {0}\n", longestWord);
    Console.WriteLine(bookStats.ToString(), "Book Info");
}

string[] FindTenMostCommon(string[] words) {
    var freqOrder = from word in words
                    where word.Length > 6
                    group word by word into g
                    orderby g.Count() descending
                    select g.Key;
    return [.. (freqOrder.Take(10))];
}

string FindLongestWord(string[] words) {
    return (from word in words orderby word.Length descending select word).FirstOrDefault("Nothing Found");
}

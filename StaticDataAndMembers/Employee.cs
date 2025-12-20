using System;

namespace EmployeeApp;

public class Employee
{
    private string _employeeName;
    private int _employeeId;
    private int _employeeAge;
    private float _currPay;
    private string _employeeSSN;


    // Auto generated using 'prop' and two tabs.
    public int MyProperty { get; set; }

    // you can use the automatic properties immediately, the backing fields are assigned a safe default, null for reference types.

    // readonly automatic property -> set only in constructor

    public int ReadOnlyProperty { get; }

    private EmployeePayTypeEnum _payType;

    public static double curInterestRate = 0.04;

    public static double InterestRate { 
        get => curInterestRate;
        set => curInterestRate = value;
    }

    public Employee() { }
    public Employee(string name, int id, float pay, int age, EmployeePayTypeEnum payType)
    {
        Name = name;
        Age = age;
        Id = id;
        Pay = pay;
        PayType = payType;
        ReadOnlyProperty = 100; // only permissable place to assign readonly automatic property.
    }

    public Employee(string name, int id, float pay) :this(name, id, pay, 0, EmployeePayTypeEnum.Salaried) { }

    //public string GetName() => _employeeName;

    //public void SetName(string name) { 
    //    if (name.Length > 15) {
    //        Console.WriteLine("Error! Name length exceeds 15 characters!");
    //    } else {
    //        _employeeName = name;
    //    }
    //}

    // How about we try C# "properties"?
    // Accessor and mutator defined right inside
    public EmployeePayTypeEnum PayType { 
        get { return _payType; }
        set { _payType = value; }
    }
    public string Name { 
        get { return _employeeName; }
        set {
            // value is a contextual keyword representing value assigned by caller
            // always the same datatype as the property.
            if (value.Length > 15) {
                Console.WriteLine("Error! Name length exceeds 15 characters!");
            } else {
                _employeeName = value;
            }
        }
    }

    public int Id {
        get { return _employeeId;  }
        set { _employeeId = value; }
    }

    public float Pay {
        get { return _currPay; }
        set { _currPay = value; }
    }

    public int Age { 
        get { return _employeeAge; }
        set { _employeeAge = value; }
    }

    // readonly properties just don't have 'set' defined.
    //public string SSN { 
    //    get { return _employeeSSN;  }
    //}

    // read-write

    public string SSN { 
        get => _employeeSSN;
        private set => _employeeSSN = value;
    }

    private DateTime _hireDate;

    public DateTime HireDate {
        get => _hireDate;
        set => _hireDate = value;
    }

    //public string SSN => _employeeSSN;

    // static properties

    public void GiveBonus(float amount) {
        // you can have multiple properties in a pattern
        // matching 'this' or, our object.
        Pay = this switch
        { 
            { Age: >= 18, PayType: EmployeePayTypeEnum.Commission, HireDate: { Year: > 2020 } } => Pay + (amount * 0.1f),
            { Age: >= 18, PayType: EmployeePayTypeEnum.Hourly, HireDate: { Year: > 2020 } } => Pay += 40F * amount / 2080F,
            { Age: >= 18, PayType: EmployeePayTypeEnum.Salaried, HireDate: { Year: > 2020 } } => Pay += amount,
            _=> Pay
        };
    }

    // Extended property patterns

    public void GiveBonusTwo(float amount) {
        Pay = this switch
        {
            { Age: >= 18, PayType: EmployeePayTypeEnum.Commission, HireDate.Year: > 2020 } => Pay + (amount * 0.1f),
            { Age: >= 18, PayType: EmployeePayTypeEnum.Hourly, HireDate.Year: > 2020 } => Pay += 40F * amount / 2080F,
            { Age: >= 18, PayType: EmployeePayTypeEnum.Salaried, HireDate.Year: > 2020 } => Pay += amount,
            _ => Pay
        };
    }

    // AUTOMATIC properties. What if we had 9 data fields and they all were just get public int Age { 
//    get { return _employeeAge; }
//set { _employeeAge = value; }, you can do { get; set; }
//    }


    public void DisplayStats() {
        Console.WriteLine($"Name: {_employeeName}");
        Console.WriteLine($"ID: {_employeeId}");
        Console.WriteLine($"Pay: {_currPay}");
        Console.WriteLine($"Age: {_employeeAge}");
    }
}

public enum EmployeePayTypeEnum { 
    Hourly, Salaried, Commission
}

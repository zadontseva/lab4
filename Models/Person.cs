using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using System.Xml.Serialization;

namespace Zadontseva_01.Models
{
    [Serializable]
    public class Person
    {

        #region Static members

        private static XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Person>));
        public static ObservableCollection<Person> List = new ObservableCollection<Person>();
        public static Person Selected = null;

        #endregion


        #region Properties

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string BirthDate
        {
            get { return _birthDate.ToLongDateString(); }
            set { _birthDate = Convert.ToDateTime(value); }
        }

        #endregion


        #region Constructors

        public Person(string name, string surname, string email, DateTime birthDate)
        {
            if (((System.DateTime.Today - birthDate).Days / 365) > 135)
            {
                throw new PersonTooOldException("Обрана дата пізніше сьогоднішньої!");
            }
            if (birthDate > System.DateTime.Today)
            {
                throw new PersonNotBornException("Обрана дата занадто стара, тільки живі користувачі приймаються!");
            }
            if (!email.Contains<char>('@'))
            //if (!(email.IndexOf('@') < email.IndexOf('.') & email.IndexOf('.') < email.Length))
            {
                throw new PersonEmailException("Некорректна електронна пошта!");
            }

            this._name = name;
            this._surname = surname;
            this._email = email;
            this._birthDate = birthDate;

            _isAdult = ((System.DateTime.Today - birthDate).Days / 365) >= 18;
            switch (birthDate.Date.Month)
            {
                case 1:
                    _sunSign = birthDate.Date.Day < 20 ? "Стрілець" : "Козоріг";
                    break;
                case 2:
                    _sunSign = birthDate.Date.Day < 16 ? "Козоріг" : "Водолій";
                    break;
                case 3:
                    _sunSign = birthDate.Date.Day < 11 ? "Водолій" : "Риби";
                    break;
                case 4:
                    _sunSign = birthDate.Date.Day < 18 ? "Риби" : "Овен";
                    break;
                case 5:
                    _sunSign = birthDate.Date.Day < 13 ? "Овен" : "Телець";
                    break;
                case 6:
                    _sunSign = birthDate.Date.Day < 21 ? "Телець" : "Близнюки";
                    break;
                case 7:
                    _sunSign = birthDate.Date.Day < 20 ? "Близнюки" : "Рак";
                    break;
                case 8:
                    _sunSign = birthDate.Date.Day < 10 ? "Рак" : "Лев";
                    break;
                case 9:
                    _sunSign = birthDate.Date.Day < 16 ? "Лев" : "Діва";
                    break;
                case 10:
                    _sunSign = birthDate.Date.Day < 30 ? "Діва" : "Терези";
                    break;
                case 11:
                    _sunSign = birthDate.Date.Day < 23 ? "Терези" : birthDate.Date.Day < 29 ? "Скорпіон" : "Змієносець";
                    break;
                case 12:
                    _sunSign = birthDate.Date.Day < 17 ? "Змієносець" : "Стрілець";
                    break;
            }
            switch (birthDate.Date.Year % 12)
            {
                case 0:
                    _chineeseSign = "Мавпа";
                    break;
                case 1:
                    _chineeseSign = "Півень";
                    break;
                case 2:
                    _chineeseSign = "Собака";
                    break;
                case 3:
                    _chineeseSign = "Свиня";
                    break;
                case 4:
                    _chineeseSign = "Пацюк";
                    break;
                case 5:
                    _chineeseSign = "Бик";
                    break;
                case 6:
                    _chineeseSign = "Тигр";
                    break;
                case 7:
                    _chineeseSign = "Кролик";
                    break;
                case 8:
                    _chineeseSign = "Дракон";
                    break;
                case 9:
                    _chineeseSign = "Змія";
                    break;
                case 10:
                    _chineeseSign = "Кінь";
                    break;
                case 11:
                    _chineeseSign = "Коза";
                    break;
            }
            _isBirthday = birthDate.Date.Month == System.DateTime.Today.Month && birthDate.Date.Day == System.DateTime.Today.Day;
        }

        public Person(string name, string surname, string email)
        {
            new Person(name, surname, email, System.DateTime.Today);
        }

        public Person(string name, string surname, DateTime birthDate)
        {
            new Person(name, surname, "default@gmail.com", birthDate);
        }

        public Person()
        {

        }

        #endregion


        #region Read-only

        private bool _isAdult;
        private string _sunSign;
        private string _chineeseSign;
        private bool _isBirthday;

        public bool IsAdult
        {
            get { return _isAdult; }
            set { _isAdult = value; }
        }
        public string SunSign
        {
            get { return _sunSign; }
            set { _sunSign = value; }
        }
        public string ChineeseSign
        {
            get { return _chineeseSign; }
            set { _chineeseSign = value; }
        }
        public bool IsBirthday
        {
            get { return _isBirthday; }
            set { _isBirthday = value; }
        }

        #endregion

        
        #region Methods

        static public ObservableCollection<Person> Generate()
        {
            if (File.Exists("people.xml"))
            {
                return Read();
            }

            string[] names = { "Іван", "Тарас", "Марія", "Тамара", "Ігор", "Георгій", "Володимир", "Наталія", "Олена", "Юлія" };
            string[] surnames = { "Мельник", "Шевченко", "Бойко", "Коваленко", "Бондаренко", "Ткаченко", "Ковальчук", "Кравченко", "Олійник", "Шевчук" };
            Random rand = new Random();

            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                for (int i = 0; i < 51; i++)
                {
                    List.Add(new Person(names[rand.Next() % 10], surnames[rand.Next() % 10], RandomString(5, rand) + "@gmail.com", (new DateTime(1975, 1, 1)).AddDays(rand.Next(10000))));
                }

                formatter.Serialize(fs, List);
            }
            return List;
        }

        static public ObservableCollection<Person> Read()
        {
            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                List = (ObservableCollection<Person>)formatter.Deserialize(fs);
            }
            return List;
        }

        static public void Save()
        {
            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, List);
            }
        }



        public static string RandomString(int length, Random random)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

    }
}

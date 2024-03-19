using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using WpfApp1;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        readonly CountriesContext db;
        DataTable dt;

        public MainWindow()
        {
            InitializeComponent();
            db = new CountriesContext();
        }

        private void BuildDataGridHeader()
        {

        }

        public void GetAllCountries()
        {
            try
            {
                tBox.Text = "Information about countries:\n";
                var coutrs = db.Countries.ToList();
                foreach (var cou in coutrs)
                {
                    tBox.Text += "Country: " + cou.NameCountry + " Square (km2):" + cou.Square + "\n";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Menu_2_1_Click(object sender, RoutedEventArgs e)
        {
            GetAllCountries();
        }

        private void Menu_2_2_Click(object sender, RoutedEventArgs e)
        {
            //Отображение всех европейских стран
            tBox.Text = "All european countries:\n";
            var eu_countries = from cou in db.Countries
                               join reg in db.Regions on cou.Idregion equals reg.Id
                               where reg.Id == 1
                               select cou;

            foreach (var eu_cou in eu_countries)
            {
                tBox.Text += eu_cou.NameCountry + "\n";
            }
        }

        private void Menu_2_3_Click(object sender, RoutedEventArgs e)
        {
            //Отображение стран с буквами а или ю
            tBox.Text = "All countries with 'a' or 'u' in it`s name\n";
            var coutries = from cou in db.Countries
                           select cou;
            foreach (var cou in coutries)
            {
                if (cou.NameCountry.ToString().ToLower().IndexOf('u') >= 0 || cou.NameCountry.ToString().ToLower().IndexOf('a') >= 0) tBox.Text += cou.NameCountry + "\n";
            }
        }

        private void Menu_2_4_Click(object sender, RoutedEventArgs e)
        {
            //Отображение стран с площадью от 3М до 5М км2
            tBox.Text = "All countries with square (3-5)M km2\n";
            var coutries = from cou in db.Countries
                           where cou.Square >= 3000000 && cou.Square <= 5000000
                           select cou;
            foreach (var cou in coutries)
            {
                tBox.Text += cou.NameCountry + " has square " + cou.Square + " km2;\n";
            }
        }

        private void Menu_2_5_Click(object sender, RoutedEventArgs e)
        {
            //Отображение стран с населением больше 10М
            tBox.Text = "All countries with sum cities population more than 10M\n";
            var coutries = db.Countries
                          .Where(country => country.Cities.Sum(city => city.CityPop) > 10000000)
                          .ToList();
            foreach (var cou in coutries)
            {
                tBox.Text += cou.NameCountry + "\n";
            }
        }

        private void Menu_2_6_Click(object sender, RoutedEventArgs e)
        {
            //Топ-3 страны по количеству населения
            tBox.Text = "TOP-3 countries by population\n";
            var coutries = db.Countries
             .OrderByDescending(country => country.Cities.Sum(city => city.CityPop))
             .Take(3)
             .ToList();

            foreach (var cou in coutries)
            {
                tBox.Text += cou.NameCountry + "\n";
            }
        }

        private void Menu_2_7_Click(object sender, RoutedEventArgs e)
        {
            //Страна с наибольшим населением
            tBox.Text = "TOP-1 countries by population\n";
            var coutries = db.Countries
             .OrderByDescending(country => country.Cities.Sum(city => city.CityPop))
             .Take(1);

            foreach (var cou in coutries)
            {
                tBox.Text += cou.NameCountry + "\n";
            }
        }

        private void Menu_2_8_Click(object sender, RoutedEventArgs e)
        {
            //Общее количество стран
            tBox.Text = "Total quantity of countries\n";
            var coutries = db.Countries.ToList();
            tBox.Text += "quantity of countries in the DB is " + coutries.Count() + ".\n";
        }

        private void Menu_3_1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Country c = new()
                {
                    NameCountry = "NewCountry",
                    Square = 999,
                    Idregion = 1
                };

                if (db.Countries.FirstOrDefault(x => x.NameCountry == c.NameCountry) == null)
                {
                    db.Countries.Add(c);
                    db.SaveChanges();
                    GetAllCountries();
                }
                else tBox.Text = "Can`t add new country";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Menu_3_2_Click(object sender, RoutedEventArgs e)
        {
            //Удаление записи "НоваяСтрана", 999
            try
            {

                Country ? c = db.Countries.FirstOrDefault(x => x.NameCountry == "NewCountry");
                if (db.Countries.FirstOrDefault(x => x.NameCountry == c.NameCountry) != null)
                {
                    db.Countries.Remove(c);
                    db.SaveChanges();
                    GetAllCountries();
                }
                else tBox.Text = "Can`t remove new country";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Menu_3_3_Click(object sender, RoutedEventArgs e)
        {
            //Изменить площадь страны NewCountry
            try
            {

                Country? c = db.Countries.FirstOrDefault(x => x.NameCountry == "NewCountry");
                c.Square = 111;
                if (db.Countries.FirstOrDefault(x => x.NameCountry == c.NameCountry) != null)
                {
                    db.Countries.Update(c);
                    db.SaveChanges();
                    GetAllCountries();
                }
                else tBox.Text = "Can`t update new country";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
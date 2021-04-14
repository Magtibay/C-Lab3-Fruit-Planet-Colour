using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Authors: Michael Asemota, Hasan Khan, Michael Matigbay, Prince Chauhan
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ComboBoxValues();
            LoadCollectionData();
            LoadCollectionDataPlanets();
            FruitDataGrid.ItemsSource = fruitCollection;
            PlanetDataGrid.ItemsSource = planetCollection;
            ThirdDataGrid.ItemsSource = linqConnection;

        }
        ObservableCollection<Fruit> fruitCollection = new ObservableCollection<Fruit>();
        ObservableCollection<Planet> planetCollection = new ObservableCollection<Planet>();
        ObservableCollection<CombinedFruitPlanet> linqConnection = new ObservableCollection<CombinedFruitPlanet>();


        List<Fruit> fruits = new List<Fruit>();
        List<Planet> planets = new List<Planet>();
        int id = 1;

        private void FirstName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fruitCB.SelectedIndex != -1)
            {
                fruitCB.Text = fruitCB.SelectedItem.ToString();
                int index = fruitCB.SelectedIndex;
                var profValue = fruits[index];
                profValue.FruitId = id;
                fruitCollection.Add(profValue);
                id++;
            }


        }

        private void SecondName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (planetCB.SelectedIndex != -1)
            {
                planetCB.Text = planetCB.SelectedItem.ToString();
                int index = planetCB.SelectedIndex;
                var profValue = planets[index];
                profValue.PlanetId = id;
                planetCollection.Add(profValue);
                id++;
            }


        }

        public void ComboBoxValues()
        {
            fruitCB.Items.Add(new ComboBoxItem("Kiwi Red"));
            fruitCB.Items.Add(new ComboBoxItem("Grapes Blue"));
            fruitCB.Items.Add(new ComboBoxItem("Dates Red"));
            fruitCB.Items.Add(new ComboBoxItem("Pear Blue"));

            planetCB.Items.Add(new ComboBoxItem("Earth Red"));
            planetCB.Items.Add(new ComboBoxItem("Jupiter Blue"));
        }


        private List<Fruit> LoadCollectionData()
        {
            fruits.Add(new Fruit()
            {
                FruitId = id,
                Name = "Kiwi",
                color = "Red",
            });

            fruits.Add(new Fruit()
            {
                FruitId = id,
                Name = "Grapes",
                color = "Blue",
            });

            fruits.Add(new Fruit()
            {
                FruitId = id,
                Name = "Dates",
                color = "Red",
            });

            fruits.Add(new Fruit()
            {
                FruitId = id,
                Name = "Pear",
                color = "Blue",
            });


            return fruits;
        }

        private List<Planet> LoadCollectionDataPlanets()
        {
            planets.Add(new Planet()
            {
                PlanetId = id,
                Name = "Earth",
                color = "Red",
            });

            planets.Add(new Planet()
            {
                PlanetId = id,
                Name = "Jupiter",
                color = "Blue",
            });

            return planets;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            fruitCB.Text = "";
            planetCB.Text = "";

            fruitCollection.Clear();
            planetCollection.Clear();
            linqConnection.Clear();

        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = FruitDataGrid.SelectedItem;
            if (selectedItem != null)
            {

                Fruit val = (Fruit)selectedItem;
                fruitCollection.Remove(val);
                fruitCB.Text = "";

            }

            var selectedItem1 = PlanetDataGrid.SelectedItem;
            if (selectedItem1 != null)
            {

                Planet val = (Planet)selectedItem1;
                planetCollection.Remove(val);
                planetCB.Text = "";

            }

        }


        private void FruitDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            PlanetDataGrid.UnselectAllCells();
            //PlanetDataGrid.Items.Refresh();
        }

        private void PlanetDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            FruitDataGrid.UnselectAllCells();
            //FruitDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            linqConnection.Clear();
            var query =
                from value in fruitCollection
                select value.Name;

            foreach (var element in query)
            {
                CombinedFruitPlanet item = new CombinedFruitPlanet();
                item.FruitName = element;
                linqConnection.Add(item);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            linqConnection.Clear();
            var query = from value in fruitCollection
                        where value.color.Equals("Red")
                        select value.Name;

            foreach (var element in query)
            {
                CombinedFruitPlanet item = new CombinedFruitPlanet();
                item.FruitName = element;
                linqConnection.Add(item);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            linqConnection.Clear();
            var query = from value in fruitCollection
                        orderby value.Name
                        select value.Name;

            foreach (var element in query)
            {
                CombinedFruitPlanet item = new CombinedFruitPlanet();
                item.FruitName = element;
                linqConnection.Add(item);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
                linqConnection.Clear();

            var query = (from f in fruitCollection
                         join p in planetCollection on f.color equals p.color
                         select new
                         {
                             fruitName = f.Name,
                             planetName = p.Name
                         });


            foreach (var element in query)
            {
                CombinedFruitPlanet item = new CombinedFruitPlanet();
                item.FruitName = element.fruitName;
                item.PlanetName = element.planetName;
                linqConnection.Add(item);
            }

        }
    }



    public class ComboBoxItem

    {
        public string Text;

        public ComboBoxItem(string text)

        {
            if (text != null)
            {
                Text = text;
            }
        }

        public override string ToString()

        {

            return Text;

        }

    }

    public class Fruit
    {
        public int FruitId { get; set; }
        public string Name { get; set; }
        public string color { get; set; }

    }

    public class Planet
    {
        public int PlanetId { get; set; }
        public string Name { get; set; }
        public string color { get; set; }

    }

    public class CombinedFruitPlanet
    {
        public string FruitName { get; set; }
        public string PlanetName { get; set; }

    }
}


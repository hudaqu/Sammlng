using System;
using System.Collections.Generic;
using System.IO;
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

namespace Sammlung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 

    //Hab mal ein Kommentar versteckt
    public partial class MainWindow : Window
    {
        //FILTER
        CustomPopup boardFilter;
        CustomPopup commandFilter;

        public MainWindow()
        {
            InitializeComponent();

            boardFilter = new CustomPopup();
            FilterBoard.Child = boardFilter;

            commandFilter = new CustomPopup();
            FilterCommand.Child = commandFilter;

            for (int i = 0; i < 25; i++)
            {
                boardFilter.AddElement(i.ToString(), "#FFFFFFFF");
            } 
            
            for (int i = 25; i > 0; i--)
            {
                commandFilter.AddElement(i.ToString(), "#FFFFFFFF");
            }
        }


        private void hashtable()
        {
            System.Collections.Hashtable h = new System.Collections.Hashtable();

            h.Add("a", "Banane");
            h.Add("b", "Erdbeere");
            h.Add("c", "Orange");
        }

        int i = 0;
        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Primitives.RepeatButton button = sender as System.Windows.Controls.Primitives.RepeatButton;

            button.Content = i++;
        }

        private void treeviewer()
        {
            foreach (string str in Environment.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = str;
                item.Tag = str;
                item.Items.Add(null);
                treeView.Items.Add(item);
            }
        }

        private void treeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;

            if (item != null)
            {
                if (item.Items.Count == 1 && item.Items[0] == null)
                {
                    item.Items.Clear();
                    try
                    {
                        foreach (string str in Directory.GetDirectories(item.Tag.ToString()))
                        {
                            TreeViewItem subitem = new TreeViewItem();
                            subitem.Header = str.Substring(str.LastIndexOf('\\') + 1);
                            subitem.Tag = str;
                            subitem.Items.Add(null);
                            item.Items.Add(subitem);
                        }
                    }
                    catch (UnauthorizedAccessException ex) { }
                    catch (InvalidOperationException ex) { }
                }
            }
        }

        private void InitDatagrid()
        {
            List<GridviewPerson> gvp = new List<GridviewPerson>();
            gvp.Add(new GridviewPerson() { FirstName = "Hung", LastName = "DangQuoc", Birthday = DateTime.Now});
            gvp.Add(new GridviewPerson() { FirstName = "Lea", LastName = "Michel-Döbler", Birthday = DateTime.Now});
            gvp.Add(new GridviewPerson() { FirstName = "Nam", LastName = "DangQuoc", Birthday = DateTime.Now});
            gvp.Add(new GridviewPerson() { FirstName = "Kevin", LastName = "Koch", Birthday = DateTime.Now});

            foreach (var item in gvp)
            {
                dataGrid.Items.Add(item);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            treeviewer();
            InitDatagrid();
            initProgbar();
            createImage();

        }

        private Image createImage()
        {
            string filepath = System.IO.Path.Combine(@"C:\Users\Surface\source\repos\Sammlung\Sammlung\pic\bsp.jpg");
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(filepath, UriKind.Absolute);
            bi.EndInit();

            Image img = new Image();
            img.Source = bi;

            return img;
        }

        private void initProgbar()
        {
            progbar.Value = 150;
            progbartext.Content = "75%";
        }

        private void punkte_dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            GridviewPerson RowData = (GridviewPerson)e.Row.Item;

            switch (RowData.FirstName)
            {
                case "Lea": e.Row.Background = new SolidColorBrush(Colors.Green); break;
                case "Nam": e.Row.Background = new SolidColorBrush(Colors.Blue); break;
                case "Kevin": e.Row.Background = new SolidColorBrush(Colors.Red); break;
                default:
                    break;
            }
            
        }

        private void FilterBoardButton_Click(object sender, RoutedEventArgs e)
        {
            FilterBoard.IsOpen = !FilterBoard.IsOpen;
            FilterCommand.IsOpen = false;
        }

        private void FilterCommandButton_Click(object sender, RoutedEventArgs e)
        {
            FilterCommand.IsOpen = !FilterCommand.IsOpen;
            FilterBoard.IsOpen = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sammlung
{
    public class CustomPopup : UserControl
    {
        private StackPanel area;
        public Action Checked;
        public Dictionary<string, bool> states;
        public Dictionary<string, Border> checkboxes;

        public CustomPopup()
        {
            area = new StackPanel();
            states = new Dictionary<string, bool>();
            checkboxes = new Dictionary<string, Border>();
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.Content = area;
            this.AddChild(scrollViewer);

            AddSearchElement();
            AddElement("Alle", "#FF1D1D1D");
        }

        private void AddSearchElement()
        {
            Border border = new Border();
            TextBox TextBox = new TextBox();
            TextBox.TextChanged += new TextChangedEventHandler(((object sender, TextChangedEventArgs e) => {
                foreach (string state in states.Keys.ToArray())
                {
                    if (!state.ToLower().Contains(TextBox.Text.ToLower()))
                    {
                        checkboxes[state].Visibility = Visibility.Hidden;
                        checkboxes[state].Height = 0;
                    }
                    else
                    {
                        checkboxes[state].Visibility = Visibility.Visible;
                        checkboxes[state].Height = Double.NaN;
                    }
                }
            }));
            border.Child = TextBox;
            area.Children.Add(border);
        }

        public void AddElement(string name)
        {
            AddElement(name, "#FF0E0E0E");
        }

        public void AddElement(string name, string color)
        {
            Border border = new Border();
            CheckBox checkBox = new CheckBox();
            TextBlock label = new TextBlock();
            label.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            label.Text = name;
            checkBox.Content = label;
            checkBox.IsChecked = true;
            checkBox.Checked += new RoutedEventHandler((object sender, RoutedEventArgs e) => ChangeState(name));
            checkBox.Unchecked += new RoutedEventHandler((object sender, RoutedEventArgs e) => ChangeState(name));
            border.Child = checkBox;
            states.Add(name, true);
            area.Children.Add(border);
            checkboxes.Add(name, border);
        }

        private void ChangeState(string name)
        {
            if (name == "Alle")
            {
                int StatesChecked = 0;
                int StatesUnChecked = 0;
                foreach (bool check in states.Values.ToArray())
                {
                    if (check)
                    {
                        StatesChecked++;
                    }
                    else
                    {
                        StatesUnChecked++;
                    }
                }
                foreach (Border b in checkboxes.Values.ToArray())
                {
                    ((CheckBox)b.Child).IsChecked = StatesChecked < StatesUnChecked;
                }
                Checked();
            }
            else
            {
                states[name] = !states[name];
                Checked();
            }
        }

        public Dictionary<string, bool> GetStates()
        {
            return states;
        }
    }
}

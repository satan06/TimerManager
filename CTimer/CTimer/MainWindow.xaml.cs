using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace CTimer
{
    public partial class MainWindow : Window
    {
        STimeManager manager = new STimeManager();      
        public MainWindow()
        {
            InitializeComponent();

            setTime.Text = DateTime.Now.ToString("HH:mm:ss");
            daysCheckBox.IsChecked = true;
            hoursCheckBox.IsChecked = true;
            minutesCheckBox.IsChecked = true;
            secondsCheckBox.IsChecked = true;
        
        }
        private void AddTimerClick(object sender, RoutedEventArgs e)
        {
            string selectedDateTime = Convert.ToDateTime(setDate.SelectedDate).ToString("dd.MM.yyyy") + 
                setTime.GetLineText(0).Insert(0, " ");

            if(inputName.Text.Length == 0)
                exeptionMessages.Content = "\'Name\' form should be filled!";
            else if (setDate.Text.Length == 0)
                exeptionMessages.Content = "\'Date\' form should be filled!";
            else if (setTime.Text.Length == 0)
                exeptionMessages.Content = "\'Time\' form should be filled!";
            else
            {
                manager.AddTimer(inputName.GetLineText(0), Convert.ToDateTime(selectedDateTime));
                timersList.Items.Add(inputName.GetLineText(0));
                currentTimer.Text = "Current time: " + manager.GetTimer.GetTimeRemaining.ToString("dd") + 
                    " days " + manager.GetTimer.GetTimeRemaining.ToString("HH:mm:ss");
                NameL.Content = manager.GetTimer.Name;
                inputName.Clear();
                setDate.Text = "";
                exeptionMessages.Content = "";
            }
        }
        private void TimersListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(timersList.SelectedIndex != -1)
            {
                manager.CurrentTimer = timersList.SelectedIndex;
                EditTimerRepresentation();
            }
        }
        private void RenameTimerClick(object sender, RoutedEventArgs e)
        {
            manager.ModifyTimer(Microsoft.VisualBasic.Interaction.InputBox("Input new name."));
            NameL.Content = "Current timer: " +  manager.GetTimer.Name;
            timersList.Items[timersList.SelectedIndex] = 
                timersList.Items[timersList.SelectedIndex].ToString().Replace(timersList.Items[timersList.SelectedIndex].ToString(), manager.GetTimer.Name);
        }
        private void DeleteTimerClick(object sender, RoutedEventArgs e)
        {
            string message = "Timer \"" + manager.GetTimer.Name + "\" will be deleted. Continue?",
                   caption = "Deleting element";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result;

            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                timersList.Items.RemoveAt(timersList.SelectedIndex);
                manager.DeleteCurrentTimer();
                currentTimer.Text = "Current time: ";
            }
        }
        private void EditTimerClick(object sender, RoutedEventArgs e)
        {
            string changedDateTime = Microsoft.VisualBasic.Interaction.InputBox("Input new DateTime (dd.MM.yyyy HH:mm:ss): ");

            manager.ModifyTimer(Convert.ToDateTime(changedDateTime));
            currentTimer.Text = "Current time: " + manager.GetTimer.GetTimeRemaining.ToString();
        }
        private void EditTimerRepresentation()
        {
            if(daysCheckBox.IsChecked == true && hoursCheckBox.IsChecked == true 
                && minutesCheckBox.IsChecked == true && secondsCheckBox.IsChecked == true)
            {
                currentTimer.Text = "Current time: " + manager.GetTimer.GetTimeRemaining.ToString("dd") +
                    " days " + manager.GetTimer.GetTimeRemaining.ToString("HH:mm:ss");
                NameL.Content = "Current timer: " + manager.GetTimer.Name;
            }
            else if (hoursCheckBox.IsChecked == true && minutesCheckBox.IsChecked == true && secondsCheckBox.IsChecked == true)
            {
                currentTimer.Text = "Current time: " + manager.GetTimer.GetTimeRemaining.ToString("HH:mm:ss");
                NameL.Content = "Current timer: " + manager.GetTimer.Name;
            }
            else if (minutesCheckBox.IsChecked == true && secondsCheckBox.IsChecked == true)
            {
                currentTimer.Text = "Current time: " + manager.GetTimer.GetTimeRemaining.ToString("mm:ss");
                NameL.Content = "Current timer: " + manager.GetTimer.Name;
            }
            else if(secondsCheckBox.IsChecked == true)
            {
                currentTimer.Text = "Current time: " + manager.GetTimer.GetTimeRemaining.ToString("ss");
                NameL.Content = "Current timer: " + manager.GetTimer.Name;
            }
        }
        private void SaveList(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                FileName = "Document",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };
            dlg.ShowDialog();

            StreamWriter file = new StreamWriter(@dlg.FileName);
            XmlSerializer serializer = new XmlSerializer(typeof(List<STimer>));

            serializer.Serialize(file, manager.Timers);
            file.Close();
        }
        private void OpenList(object sender, RoutedEventArgs e)
        {
            manager.Timers.Clear();

            OpenFileDialog dlg = new OpenFileDialog
            {
                FileName = "Document",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };
            dlg.ShowDialog();

            StreamReader file = new StreamReader(@dlg.FileName);
            XmlSerializer serializer = new XmlSerializer(typeof(List<STimer>));

            manager.Timers = (List<STimer>)serializer.Deserialize(file);
            file.Close();

            foreach (var timer in manager.Timers)
                timersList.Items.Add(manager.GetTimer.Name);
        }
        private void ClearListClick(object sender, RoutedEventArgs e)
        {
            string message = "All timers will be deleted. Continue?",
                   caption = "Clear list";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result;

            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                manager.Timers.Clear();
                timersList.Items.Clear();
                currentTimer.Text = "Current time: ";
            }
        }
    }
}

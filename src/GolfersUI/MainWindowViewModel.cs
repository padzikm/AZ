using Golfers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GolfersUI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Points = new ObservableCollection<Golfers.Point>();
            Solution = new ObservableCollection<Tuple<Golfers.Point, Golfers.Point>>();
            IsReady = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public ICommand LoadFileCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var dialog = new OpenFileDialog();
                    dialog.DefaultExt = ".txt";
                    dialog.Filter = "Text files (*.txt)|*.txt";
                    dialog.Title = "Wczytaj dane z pliku";
                    var result = dialog.ShowDialog();

                    if (result != true)
                        return;

                    var dataIO = new DataIO();

                    try
                    {
                        var loadedPoints = dataIO.Read(dialog.FileName);
                        Points = new ObservableCollection<Golfers.Point>(loadedPoints);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nieprawidłowa zawartość pliku", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        public ICommand SaveFileCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if(Solution == null || Solution.Count == 0)
                    {
                        MessageBox.Show("Brak punktów w rozwiązaniu", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var dialog = new SaveFileDialog();
                    dialog.DefaultExt = ".txt";
                    dialog.Filter = "Text files (*.txt)|*.txt";
                    dialog.Title = "Wczytaj dane z pliku";
                    var result = dialog.ShowDialog();

                    if (result != true)
                        return;

                    var dataIO = new DataIO();

                    try
                    {
                        dataIO.Write(dialog.FileName, solution.ToList());
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Błąd przy zapisywaniu do pliku", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Application.Current.MainWindow.Close();
                });
            }
        }

        public ICommand SolveCommand
        {
            get
            {
                return new RelayAsyncCommand(() =>
                {
                    IsReady = false;

                    if (Points == null || Points.Count == 0)
                    {
                        MessageBox.Show("Brak punktów do połączenia", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsReady = true;
                        return;
                    }

                    var validator = new Validator();
                    if(!validator.IsDataValid(Points.ToList()))
                    {
                        MessageBox.Show("Punkty są niepoprawne", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsReady = true;
                        return;
                    }

                    var algorithm = new Algorithm();
                    var solution = algorithm.Solve(Points.ToList());

                    if(!validator.IsSolutionValid(solution))
                    {
                        MessageBox.Show("Rozwiązanie jest niepoprawne", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsReady = true;
                        return;
                    }

                    Solution = new ObservableCollection<Tuple<Golfers.Point, Golfers.Point>>(solution);
                    IsReady = true;

                });
            }
        }

        private ObservableCollection<Golfers.Point> points;
        public ObservableCollection<Golfers.Point> Points
        {
            get { return points; }
            set 
            { 
                points = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Points"));
            
            }
        }

        private ObservableCollection<Tuple<Golfers.Point, Golfers.Point>> solution;
        public ObservableCollection<Tuple<Golfers.Point, Golfers.Point>> Solution
        {
            get { return solution; }
            set 
            { 
                solution = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Solution"));

            }
        }

        private bool isReady;
        public bool IsReady
        {
            get { return isReady; }
            set 
            { 
                isReady = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsReady"));
            }
        }



    }
}

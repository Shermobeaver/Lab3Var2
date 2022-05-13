using System;
using System.Windows;
using ViewModel;

namespace Lab2Var2
{
    public partial class MainWindow : Window
    {
        public ThisViewActions ProvidedActions { get; set; }
        public ViewData ViewModel { get; set; }

        public MainWindow()
        {
            try
            {
                ProvidedActions = new();
                ViewModel = new(ProvidedActions);

                DataContext = this;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            InitializeComponent();

            Func.ItemsSource = Enum.GetValues(typeof(Model.SPf));
        }

        private Commands commandMeasure;
        public Commands CommandMeasure
        {
            get
            {
                return commandMeasure ??
                    (commandMeasure = new Commands(ViewModel.ActionMeasure, ViewModel.ActionMeasure_CanExecute));
            }
        }

        private Commands commandSplines;
        public Commands CommandSplines
        {
            get
            {
                return commandSplines ??
                    (commandSplines = new Commands(ViewModel.ActionSplines, ViewModel.ActionSplines_CanExecute));
            }
        }
    }
}

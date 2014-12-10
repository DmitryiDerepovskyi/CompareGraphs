using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Globalization;

namespace CompareGraphs
{
    /// Логика взаимодействия для MainWindow.xaml
    public partial class MainWindow : Window
    {
        ObservableCollection<Element> _listFirstP, _listFirstG, _listSecondP, _listSecondG;
        bool _isFirstGraphInput, _isSecondGraphInput;
        Graph _graphFirst, _graphSecond;
        Image _imageFG = new Image();
        Image _imageSG = new Image();

        public MainWindow()
        {
            InitializeComponent();
            _listFirstP = new ObservableCollection<Element>();
            _listFirstG = new ObservableCollection<Element>();
            _listSecondP = new ObservableCollection<Element>();
            _listSecondG = new ObservableCollection<Element>();
            dgFirstGraphG.ItemsSource = _listFirstG;
            dgFirstGraphP.ItemsSource = _listFirstP;
            dgSecondGraphP.ItemsSource = _listSecondP;
            dgSecondGraphG.ItemsSource = _listSecondG;
        }

        #region InputData 
        /// Выбор файла с данными для первого графа
        private void btnSelectFirstGraphFile_Click(object sender, RoutedEventArgs e)
        {
            //получаем имя файла
            string fname = OpenFile();
            if (fname.Length != 0)
            {
                //вызываем метод для сохранения массива в файл
                ReadFile(fname,out _listFirstG,out _listFirstP);
                dgFirstGraphG.ItemsSource = _listFirstG;
                dgFirstGraphP.ItemsSource = _listFirstP;
            }
        }
        /// Выбор файла с данными для второго графа
        private void btnSelectSecondGraphFile_Click(object sender, RoutedEventArgs e)
        {
            //получаем имя файла
            string fname = OpenFile();
            if (fname.Length != 0)
            {
                //вызываем метод для сохранения массива в файл
                ReadFile(fname, out _listSecondG, out _listSecondP);
                dgSecondGraphG.ItemsSource = _listSecondG;
                dgSecondGraphP.ItemsSource = _listSecondP;
            }
        }
        /// Чтение с файла
        void ReadFile(string filename, out ObservableCollection<Element> g, out ObservableCollection<Element> p)
        {
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    var sr = new StreamReader(fs);
                    var strG = sr.ReadLine().Trim();
                    var strP = sr.ReadLine().Trim();
                    g = SplitStringToList(strG);
                    p = SplitStringToList(strP);
                }
            }
            catch(Exception)
            {
                g = null;
                p = null;
                MessageBox.Show("Invalid data in the file");
            }
          
        }

        /// Разделяем строку на массив чисел
        private ObservableCollection<Element> SplitStringToList(string list)
        {
            var collection = new ObservableCollection<Element>();
            var split = list.Split(' ');
            foreach (var s in split)
            {
                if (s == String.Empty)
                    continue;
                var elem = new Element();
                var num = Int32.Parse(s);
                if (num < 0)
                {
                    throw new FormatException();
                }
                elem.Value = num;
                collection.Add(elem);
            }
            return collection;
        }

        /// Выбор файла с данными 
        string OpenFile()
        {
            var fname = String.Empty;
            var myDialog = new OpenFileDialog
            {
                Filter = "txt files(*.txt)|*.txt|All files(*.*)|*.*",
                CheckFileExists = true
            };
            //Проверка существования файла с заданным именем
            if (myDialog.ShowDialog() == true)
            {
                fname = myDialog.FileName;
            }
                //проверка имени файла
                if (fname.Length == 0)
                    return fname;
                // размер массива
            return fname;
        }
        #endregion

        #region ClearDataGrid
        /// Очистка содержимого таблицы и панели для первого графа
        private void btnClearFirstGraph_Click(object sender, RoutedEventArgs e)
        {
            csFirstGraph.Children.Clear();
            _listFirstG.Clear();
            _listFirstP.Clear();
        }

       /// Очистка содержимого таблицы и панели для второго графа
        private void btnClearSecondGraph_Click(object sender, RoutedEventArgs e)
        {
            csSecondGraph.Children.Clear();
            _listSecondG.Clear();
            _listSecondP.Clear();
        }
        #endregion
        /// Сравнение графов по их харектеристике - живучесть
        private void btnCompareGraphs_Click(object sender, RoutedEventArgs e)
        {
            if(_isSecondGraphInput && _isFirstGraphInput)
            {
                if(_graphFirst == _graphSecond)
                    MessageBox.Show(string.Format("Graphs are equivalent\nVitality {0} ==  {1}",
                        _graphFirst.GetVitality(), _graphSecond.GetVitality()));
                else
                    MessageBox.Show(string.Format("Graphs are not equivalent\nVitality {0} != {1}"
                                                  , _graphFirst.GetVitality(), _graphSecond.GetVitality()));
            }
        }
        /// Подтверждение ввода данных для первого графа
        private void btnOkFirstGraph_Click(object sender, RoutedEventArgs e)
        {
            var arrG = ConvertObservableToArray(_listFirstG);
            var arrP = ConvertObservableToArray(_listFirstP);
            if (arrG.Length == 0)
            {
                MessageBox.Show("G is empty!");
                return;
            }
            if (arrP.Length == 0)
            {
                MessageBox.Show("P is empty!");
                return;
            }
            try
            {
                _graphFirst = new Graph(arrG, arrP);
                VisualizationOfGraph.PrintGraph(ref csFirstGraph, _graphFirst.GetAdjacencyMatrix());
                _imageFG.Source = VisualizationOfGraph.ToImageSource(csFirstGraph);
                csFirstGraph.Children.Clear();
                Canvas.SetLeft(_imageFG, 0);
                Canvas.SetTop(_imageFG, 0);
                csFirstGraph.Children.Add(_imageFG);
                _isFirstGraphInput = true;
            }
            catch (IncorrectDataException ex)
            {
                _isSecondGraphInput = false;
                MessageBox.Show(ex.Message);
            }
        }
        /// Подтверждение данный для второго графа
        private void btnOkSecondGraph_Click(object sender, RoutedEventArgs e)
        {
            var arrG = ConvertObservableToArray(_listSecondG);
            var arrP = ConvertObservableToArray(_listSecondP);
            if (arrG.Length == 0)
            {
                MessageBox.Show("G is empty!");
                return;
            }
            if (arrP.Length == 0)
            {
                MessageBox.Show("P is empty!");
                return;
            }
            try
            {
                _graphSecond = new Graph(arrG, arrP);
                VisualizationOfGraph.PrintGraph(ref csSecondGraph, _graphSecond.GetAdjacencyMatrix());
                _imageSG.Source = VisualizationOfGraph.ToImageSource(csSecondGraph);
                csSecondGraph.Children.Clear();
                Canvas.SetLeft(_imageSG, 0);
                Canvas.SetTop(_imageSG, 0);
                csSecondGraph.Children.Add(_imageSG);
                _isSecondGraphInput = true;
            }
            catch(IncorrectDataException ex){
                _isSecondGraphInput = false;
                MessageBox.Show(ex.Message);
            }
        }

        /// Конвертирование List<Element> в int[] по свойству Value 
        int[] ConvertObservableToArray(ObservableCollection<Element> list)
        {
            var array = new int[list.Count];
            for (var i = 0; i < list.Count; i++)
                array[i] = list[i].Value;
            return array;
        }
        /// Обработка вводимого текста в ячейки таблицы
        private void DgPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text[e.Text.Length - 1]))
            {
                //запретить ввод не цифр
                e.Handled = true;
            }
        }
        /// Изменение размера изображения 
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            csFirstGraph.Children.Clear();
            csSecondGraph.Children.Clear();
            if (csFirstGraph.ActualWidth > csFirstGraph.ActualHeight)
            {
                _imageFG.Height = _imageFG.Width = csFirstGraph.ActualHeight;
                _imageSG.Height = _imageSG.Width = csFirstGraph.ActualHeight;
            }
            else
            {
                _imageFG.Height = _imageFG.Width = csFirstGraph.ActualWidth;
                _imageSG.Height = _imageSG.Width = csFirstGraph.ActualWidth;
            }
            csFirstGraph.Children.Add(_imageFG);
            csSecondGraph.Children.Add(_imageSG);
        }

    }
}

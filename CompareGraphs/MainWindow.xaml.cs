using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;

namespace CompareGraphs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Element> _listFirstP, _listFirstG, _listSecondP, _listSecondG;
        bool _isFirstGraphInput = false, _isSecondGraphInput = false;
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
        /// <summary>
        /// Выбор файла с данными для первого графа
        /// </summary>
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
        /// <summary>
        /// Выбор файла с данными для второго графа
        /// </summary>
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
        /// <summary>
        /// Чтение с файла
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="g"></param>
        /// <param name="p"></param>
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
            catch(Exception e)
            {
                g = null;
                p = null;
                MessageBox.Show(e.Message);
            }
          
        }

        /// <summary>
        /// Разделяем строку на массив чисел
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        ObservableCollection<Element> SplitStringToList(string list)
        {
            var collection = new ObservableCollection<Element>();
            try
            {
                var split = list.Split(' ');
                foreach (var s in split)
                {
                    if (s == String.Empty)
                        continue;
                    var elem = new Element();
                    var num = Int32.Parse(s);
                    if (num < 0) 
                        throw new FormatException();
                    elem.Value = num;
                    collection.Add(elem);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Error!!! File include incorrect data.");
            }
            return collection;
        }
        /// <summary>
        /// Выбор файла с данными 
        /// </summary>
        /// <returns></returns>
        string OpenFile()
        {
            string fname = String.Empty;
            var myDialog = new OpenFileDialog();
            myDialog.Filter = "txt files(*.txt)|*.txt|All files(*.*)|*.*";
            //Проверка существования файла с заданным именем
            myDialog.CheckFileExists = true;
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
        /// <summary>
        /// Очистка содержимого таблицы и панели для первого графа
        /// </summary>
        private void btnClearFirstGraph_Click(object sender, RoutedEventArgs e)
        {
            csFirstGraph.Children.Clear();
            _listFirstG.Clear();
            _listFirstP.Clear();
        }

       /// <summary>
       /// Очистка содержимого таблицы и панели для второго графа
       /// </summary>
        private void btnClearSecondGraph_Click(object sender, RoutedEventArgs e)
        {
            csSecondGraph.Children.Clear();
            _listSecondG.Clear();
            _listSecondP.Clear();
        }
        #endregion
        /// <summary>
        /// Сравнение графов по их харектеристике - живучесть
        /// </summary>
        private void btnCompareGraphs_Click(object sender, RoutedEventArgs e)
        {
            if(_isSecondGraphInput && _isFirstGraphInput)
            {
                if(_graphFirst == _graphSecond)
                    MessageBox.Show("Graphs are equivalent");
                else
                    MessageBox.Show("Graphs are not equivalent");
            }
        }
        /// <summary>
        /// Подтверждение ввода данных для первого графа
        /// </summary>
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
        /// <summary>
        /// Подтверждение данный для второго графа
        /// </summary>
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

        /// <summary>
        /// Конвертирование List<Element> в int[] по свойству Value 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int[] ConvertObservableToArray(ObservableCollection<Element> list)
        {
            var array = new int[list.Count];
            for (var i = 0; i < list.Count; i++)
                array[i] = list[i].Value;
            return array;
        }
        /// <summary>
        /// Обработка вводимого текста в ячейки таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text[e.Text.Length - 1]))
            {
                //запретить ввод не цифр
                e.Handled = true;
            }
        }
        /// <summary>
        /// Изменение размера изображения 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

﻿using System;
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
using Microsoft.Win32;
using System.IO;
using System.Collections.Specialized;

namespace CompareGraphs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Element> listFirstP, listFirstG, listSecondP, listSecondG;
        bool isFirstGraphInput = false, isSecondGraphInput = false;
        Graph GraphFirst, GraphSecond;
        Image bgFG = new Image();
        Image bgSG = new Image();

        public MainWindow()
        {
            InitializeComponent();
            listFirstP = new ObservableCollection<Element>();
            listFirstG = new ObservableCollection<Element>();
            listSecondP = new ObservableCollection<Element>();
            listSecondG = new ObservableCollection<Element>();
            dgFirstGraphG.ItemsSource = listFirstG;
            dgFirstGraphP.ItemsSource = listFirstP;
            dgSecondGraphP.ItemsSource = listSecondP;
            dgSecondGraphG.ItemsSource = listSecondG;
        }

        #region InputData
        private void btnSelectSecondGraphFile_Click(object sender, RoutedEventArgs e)
        {
            //получаем имя файла
            string fname = OpenFile();
            if (fname.Length != 0)
            {
                //вызываем метод для сохранения массива в файл
                ReadFile(fname, out listSecondG, out listSecondP);
                dgSecondGraphG.ItemsSource = listSecondG;
                dgSecondGraphP.ItemsSource = listSecondP;
            }
        }

        private void btnSelectFirstGraphFile_Click(object sender, RoutedEventArgs e)
        {
            //получаем имя файла
            string fname = OpenFile();
            if (fname.Length != 0)
            {
                //вызываем метод для сохранения массива в файл
                ReadFile(fname,out listFirstG,out listFirstP);
                dgFirstGraphG.ItemsSource = listFirstG;
                dgFirstGraphP.ItemsSource = listFirstP;
            }
        }
        
        bool ReadFile(string filename, out ObservableCollection<Element> G, out ObservableCollection<Element> P)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                string strG = sr.ReadLine().Trim();
                string strP = sr.ReadLine().Trim();
                G = SplitStringToList(strG);
                P = SplitStringToList(strP);
            } 
            return true;
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
                string[] split = list.Split(' ');
                foreach (string s in split)
                {
                    if (s == String.Empty)
                        continue;
                    var elem = new Element();
                    int num = Int32.Parse(s);
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
            OpenFileDialog myDialog = new OpenFileDialog();
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
        private void btnClearSecondGraph_Click(object sender, RoutedEventArgs e)
        {
            listSecondG.Clear();
            listSecondP.Clear();
        }

        private void btnClearFirstGraph_Click(object sender, RoutedEventArgs e)
        {
            listFirstG.Clear();
            listFirstP.Clear();
        }
        #endregion

        private void btnCompareGraphs_Click(object sender, RoutedEventArgs e)
        {
            if(isSecondGraphInput && isFirstGraphInput)
            {
                if(GraphFirst == GraphSecond)
                    MessageBox.Show("Graphs are equivalent");
                else
                    MessageBox.Show("Graphs are not equivalent");
            }
        }

        private void btnOkFirstGraph_Click(object sender, RoutedEventArgs e)
        {
            int[] arrG, arrP;
            arrG = ConvertObservableToArray(listFirstG);
            arrP = ConvertObservableToArray(listFirstP);
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
                GraphFirst = new Graph(arrG, arrP);
                VisualizationOfGraph.PrintGraph(ref csFirstGraph, GraphFirst.GetAdjacencyMatrix());
                bgFG.Source = ConvertToImage.ToImageSource(csFirstGraph);
                csFirstGraph.Children.Clear();
                Canvas.SetLeft(bgFG, 0);
                Canvas.SetTop(bgFG, 0);
                csSecondGraph.Children.Add(bgFG);
                isFirstGraphInput = true;
            }
            catch (IncorrectDataException ex)
            {
                isSecondGraphInput = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOkSecondGraph_Click(object sender, RoutedEventArgs e)
        {
            int[] arrG, arrP;
            arrG = ConvertObservableToArray(listSecondG);
            arrP = ConvertObservableToArray(listSecondP);
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
                GraphSecond = new Graph(arrG, arrP);
                VisualizationOfGraph.PrintGraph(ref csSecondGraph, GraphSecond.GetAdjacencyMatrix());
                bgSG.Source = ConvertToImage.ToImageSource(csSecondGraph);
                csSecondGraph.Children.Clear();
                Canvas.SetLeft(bgSG, 0);
                Canvas.SetTop(bgSG, 0);
                csSecondGraph.Children.Add(bgSG);
                isSecondGraphInput = true;
            }
            catch(IncorrectDataException ex){
                isSecondGraphInput = false;
                MessageBox.Show(ex.Message);
            }
        }


        int[] ConvertObservableToArray(ObservableCollection<Element> list)
        {
            int[] array = new int[list.Count];
            for (int i = 0; i < list.Count; i++)
                array[i] = list[i].Value;
            return array;
        }

        private void dgPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text[e.Text.Length - 1]))
            {
                //запретить ввод не цифр
                e.Handled = true;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            csFirstGraph.Children.Clear();
            csSecondGraph.Children.Clear();
            if (csFirstGraph.ActualWidth > csFirstGraph.ActualHeight)
            {
                bgFG.Height = bgFG.Width = csFirstGraph.ActualHeight;
                bgSG.Height = bgSG.Width = csFirstGraph.ActualHeight;
            }
            else
            {
                bgFG.Height = bgFG.Width = csFirstGraph.ActualWidth;
                bgSG.Height = bgSG.Width = csFirstGraph.ActualWidth;
            }
            csFirstGraph.Children.Add(bgFG);
            csSecondGraph.Children.Add(bgSG);
        }

    }
}

using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCad
{
   // взято с сайта https://habr.com/ru/articles/243305/ 25-09-2023
        public class Commands : IExtensionApplication
        {
            // эта функция будет вызываться при выполнении в AutoCAD команды "TestCommand"
            [CommandMethod("TestCommand")]
            public void MyCommand()
            {
                // создаем выпадающий список
                Autodesk.Windows.RibbonCombo comboBox1 = new RibbonCombo();
                comboBox1.Id = "_combobox1";

                // создаем кнопку
                Autodesk.Windows.RibbonButton button1 = new Autodesk.Windows.RibbonButton();
                button1.Id = "_button1";
                // привязываем к кнопке обработчик нажатия
                button1.CommandHandler = new CommandHandler_Button1();

                // создаем контейнер для элементов
                Autodesk.Windows.RibbonPanelSource rbPanelSource = new Autodesk.Windows.RibbonPanelSource();
                rbPanelSource.Title = "Новая панель элементов";
                // добавляем в контейнер элементы управления
                rbPanelSource.Items.Add(comboBox1);
                rbPanelSource.Items.Add(new RibbonSeparator());
                rbPanelSource.Items.Add(button1);

                // создаем панель
                RibbonPanel rbPanel = new RibbonPanel();
                // добавляем на панель контейнер для элементов
                rbPanel.Source = rbPanelSource;

                // создаем вкладку
                RibbonTab rbTab = new RibbonTab();
                rbTab.Title = "Новая вкладка";
                rbTab.Id = "HabrRibbon";
                // добавляем на вкладку панель
                rbTab.Panels.Add(rbPanel);

                // получаем указатель на ленту AutoCAD
                Autodesk.Windows.RibbonControl rbCtrl = ComponentManager.Ribbon;
                // добавляем на ленту вкладку
                rbCtrl.Tabs.Add(rbTab);
                // делаем созданную вкладку активной ("выбранной")
                rbTab.IsActive = true;
            }

            // Функции Initialize() и Terminate() необходимы, чтобы реализовать интерфейс IExtensionApplication
            public void Initialize()
            {

            }

            public void Terminate()
            {

            }
        }

        // обработчик нажатия кнопки
        public class CommandHandler_Button1 : System.Windows.Input.ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object param)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                System.Windows.MessageBox.Show("Habr!");
            }
        }
    }


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PracticeWinForm
{
    public partial class Form1 : Form
    {


        private int ThreadID;


        ////////////////////////////////////////////////////////////////////////
        //  WinForm
        ////////////////////////////////////////////////////////////////////////


        public Form1()
        {
            InitializeComponent();
            ThreadID = Environment.CurrentManagedThreadId;
        }


        private void ProgressFunc()
        {
            ThreadID = Environment.CurrentManagedThreadId;
            Console.WriteLine($"[실습] ProgressFunc() 스레드ID : {ThreadID}");
            for (int i=0; i<= 100; i++)
            {
                Thread.Sleep(50);
                SetLabel(i);
            }
        }


        private async void SetLabel(int value)
        {
            await Task.Run(() =>
            {
                progressBar.Value = value;
                string str = value.ToString() + " %";
                percentLabel.Text = str;
            });
        }


        ////////////////////////////////////////////////////////////////////////
        //  Button Click Func
        ////////////////////////////////////////////////////////////////////////


        private void SyncButtonClick(object sender, EventArgs e)
        {
            ProgressFunc();
            MessageBox.Show($"[실습] SyncButtonClick, 스레드ID : {ThreadID}");
        }

        private void MsgButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show($"비동기 처리 실습, 스레드ID : {ThreadID}", "Message!");
        }

        private void VoidButtonClick(object sender, EventArgs e)
        {
            Task1();
            MessageBox.Show($"VoidButtonClick Progress Complete!, 스레드ID : {ThreadID}", "Message!");
        }


        private async void TaskButtonClick(object sender, EventArgs e)
        {
            await Task2();
            MessageBox.Show($"VoidButtonClick Progress Complete!, 스레드ID : {ThreadID}", "Message!");
        }


        private async void TaskIntButtonClick(object sender, EventArgs e)
        {
            int result = await Task3();
            MessageBox.Show($"VoidButtonClick Progress Complete!, 스레드ID : {ThreadID} (Result Code: {result})", "Message!");
        }


        ////////////////////////////////////////////////////////////////////////
        //  Thread Callback
        ////////////////////////////////////////////////////////////////////////


        async void Task1()
        {
            await Task.Run(() =>
            {
                ThreadID = Environment.CurrentManagedThreadId;
                Console.WriteLine($"[실습] Task1() 스레드ID : {ThreadID}");
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(50);
                    SetLabel(i);
                }
            });
        }


        async Task Task2()
        {
            await Task.Run(() =>
            {
                ThreadID = Environment.CurrentManagedThreadId;
                Console.WriteLine($"[실습] Task2() 스레드ID : {ThreadID}");
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(50);
                    SetLabel(i);
                }
            });
        }


        async Task<int> Task3()
        {
            int n = await Task.Run(() =>
            {
                ThreadID = Environment.CurrentManagedThreadId;
                Console.WriteLine($"[실습] Task3() 스레드ID : {ThreadID}");
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(50);
                    SetLabel(i);
                }
                return 200;
            });

            return n;
        }
    }
}

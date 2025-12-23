using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Calculator;

public partial class MainWindow : Window
{

    public void Add(object button, RoutedEventArgs e){
        String content = (String)((Button)button).Content;
        if (content == "CE"){
            Result.Text = "";
            Operations.Text = "";
        } else if (content == "+" || content == "-" || content == "×" || content == "÷" || content == "%"){
            if (Operations.Text != ""){
                if (Operations.Text[Operations.Text.Length - 1] != '\x20'){
                    Operations.Text += '\x20';
                    Operations.Text += content;
                    Operations.Text += '\x20';
                } else {
                    Operations.Text = Operations.Text.Substring(0, Operations.Text.Length - 2) + content + '\x20';
                }
            }
        } else if (content == "="){

        } else {
            Operations.Text += content;
        }
    }

    public MainWindow()
    {
        InitializeComponent();
    }
}
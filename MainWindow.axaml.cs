using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (Operations.Text[Operations.Text.Length - 1] != '\x20' && Operations.Text[Operations.Text.Length - 1] != '√' && Operations.Text[Operations.Text.Length - 1] != '²' && Operations.Text[Operations.Text.Length - 1] != '.'){
                bool Error = false;
                List<String> NumAndOper = Operations.Text.Split('\x20').ToList();
                for (int i = 0; i < NumAndOper.Count; i++){
                    if (Error) break;
                    if (NumAndOper[i] == "×" || NumAndOper[i] == "÷"){
                        String Answer;
                        double FistNumber = double.Parse(NumAndOper[i - 1]);
                        double SecondNumber = double.Parse(NumAndOper[i + 1]);
                        if (NumAndOper[i] == "×"){
                            Answer = (FistNumber * SecondNumber).ToString();
                        } else {
                            if (SecondNumber == 0){
                                Error = true;
                                break;
                            }
                            Answer = (FistNumber / SecondNumber).ToString();
                        }
                        NumAndOper.RemoveAt(i - 1);
                        NumAndOper.RemoveAt(i);
                        NumAndOper[i - 1] = Answer;
                        i--;
                    }
                }
                for (int i = 0; i < NumAndOper.Count; i++){
                    if (Error) break;
                    if (NumAndOper[i] == "+" || NumAndOper[i] == "-"){
                        string Answer;
                        double FistNumber = double.Parse(NumAndOper[i - 1]);
                        double SecondNumber = double.Parse(NumAndOper[i + 1]);
                        if (NumAndOper[i] == "+"){
                            Answer = (FistNumber + SecondNumber).ToString();
                        } else {
                            Answer = (FistNumber - SecondNumber).ToString();
                        }
                        NumAndOper.RemoveAt(i - 1);
                        NumAndOper.RemoveAt(i);
                        NumAndOper[i - 1] = Answer;
                        i--;
                    }
                }
                if (!Error){
                    Result.Text = NumAndOper[0];
                } else {
                    Result.Text = "ERROR";
                }
            } else {
                Result.Text = "ERROR";
            }

        // Проверку надо дороботать
        } else if(content != "." || Operations.Text.Length != 0 || Operations.Text[Operations.Text.Length - 1] != '.'){
            Operations.Text += content;
        }
    }

    public MainWindow()
    {
        InitializeComponent();
    }
}
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator;

public partial class MainWindow : Window
{

    public void AddDigit(object digit, RoutedEventArgs e){
        String content = (String)((Button)digit).Content;

        // Проверку надо дороботать
        if(content != "." || (Operations.Text.Length != 0 && Operations.Text[Operations.Text.Length - 1] != '.' && Operations.Text[Operations.Text.Length - 1] != '\x20')){
            Operations.Text += content;
        }
    }

    public void Clear(object clear, RoutedEventArgs e){
        Result.Text = "";
        Operations.Text = "";
    }

    public void AddOper(object oper, RoutedEventArgs e){
        String content = (String)((Button)oper).Content;
        if (Operations.Text != "" && Operations.Text[Operations.Text.Length - 1] != '√' && Operations.Text[Operations.Text.Length - 1] != '.'){
            if (Operations.Text[Operations.Text.Length - 1] != '\x20'){
                Operations.Text += '\x20';
                Operations.Text += content;
                Operations.Text += '\x20';
            } else {
                Operations.Text = Operations.Text.Substring(0, Operations.Text.Length - 2) + content + '\x20';
            }
        }
    }

    public void AddPower(object power, RoutedEventArgs e){
        if (Operations.Text != "" && Operations.Text[Operations.Text.Length - 1] != '√'&& Operations.Text[Operations.Text.Length - 1] != '²' && Operations.Text[Operations.Text.Length - 1] != '.' && Operations.Text[Operations.Text.Length - 1] != '\x20'){
            Operations.Text += '²';
        }
    }

    public void AddSqrt(object sqrt, RoutedEventArgs e){
        if (Operations.Text == "" || Operations.Text[Operations.Text.Length - 1] == '\x20'){
            Operations.Text += '√';
        }
    }

    public void AddPoint(object point, RoutedEventArgs e){

    }

    public void Calculate(object calc, RoutedEventArgs e){
            if (Operations.Text != "" && Operations.Text[Operations.Text.Length - 1] != '\x20' && Operations.Text[Operations.Text.Length - 1] != '√' && Operations.Text[Operations.Text.Length - 1] != '.'){
                bool Error = false;
                List<String> NumAndOper = Operations.Text.Split('\x20').ToList();

                for (int i = 0; i < NumAndOper.Count; i++){
                    if (NumAndOper[i].IndexOf('²') != -1){
                        if (NumAndOper[i].IndexOf('√') != -1){
                            NumAndOper[i] = NumAndOper[i].Substring(1, NumAndOper[i].Length - 1);
                        } else {
                            NumAndOper[i] = Math.Pow(double.Parse(NumAndOper[i].Substring(0, NumAndOper[i].Length - 1)), 2).ToString();
                        }
                    } else if (NumAndOper[i].IndexOf('√') != -1){
                        NumAndOper[i] = Math.Sqrt(double.Parse(NumAndOper[i].Substring(1, NumAndOper[i].Length - 1))).ToString();
                    }
                }

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
    }

    public MainWindow()
    {
        InitializeComponent();
    }
}
﻿using Pastel;
using SRM_Connectivity_of_graphs;

var matrix = new int[,]
{ 
    { 0, 1, 0, 1, 0 }, 
    { 0, 0, 0, 0, 0 },
    { 1, 1, 0, 0, 0 },
    { 0, 0, 0, 0, 1 },
    { 0, 0, 0, 0, 0 }
};

MatrixExtension.SeparateStrings();

Console.WriteLine("Початкова матриця: ".Pastel("#F8DDFA"));
matrix.DrawMatrixInConsole();

Console.WriteLine();

Console.WriteLine("\nТранспонована матриця: ".Pastel("#F8DDFA"));
var transponedMatrix = MatrixExtension.GetTransposeMatrix(matrix);
transponedMatrix.DrawMatrixInConsole();

MatrixExtension.SeparateStrings();

var resultMatrix = MatrixExtension.GetReachabilityMatrix(matrix);
Console.WriteLine("Матриця досяжностi: ".Pastel("#F8DDFA"));
resultMatrix.DrawMatrixInConsole();

MatrixExtension.SeparateStrings();

var resultMatrixShort = MatrixExtension.GetReachabilityMatrixShort(matrix);
Console.WriteLine("[КОРОТКИЙ МЕТОД] Матриця досяжностi: ".Pastel("#F8DDFA"));
resultMatrixShort.DrawMatrixInConsole();

MatrixExtension.SeparateStrings();

Console.WriteLine($"{"Чи є граф сильно-зв'язним?:".Pastel("#F8DDFA")} {(resultMatrix.IsGraphStronglyConnected() ? ("Так, граф є сильно-зв'язним.".Pastel("#1FD52D")) : ("Нi, граф не є сильно-зв'язним.".Pastel("#FF5733")))}");
Console.WriteLine($"{"Чи є граф однобiчно-зв'язним?:".Pastel("#F8DDFA")} {(resultMatrix.IsGraphUnilaterallyConnected() ? ("Так, граф є однобiчно-зв'язним.".Pastel("#1FD52D")) : ("Нi, граф не є однобiчно-зв'язним.".Pastel("#FF5733")))}");
Console.WriteLine($"{"Чи є граф слабко-зв'язним?:".Pastel("#F8DDFA")} {(matrix.IsGraphWeaklyConnected() ? ("Так, граф є слабко-зв'язним.".Pastel("#1FD52D")) : ("Нi, граф не є слабко-зв'язним.".Pastel("#FF5733")))}");

MatrixExtension.SeparateStrings();

string stream = string.Empty;
do
{
    Console.Write("\nНапишiть \"quit\" для виходу з програми: ".Pastel("#F8DDFA"));
    stream = Console.ReadLine() ?? "";
} while (stream != "quit");
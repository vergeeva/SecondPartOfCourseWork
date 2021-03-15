Public Class Form1

    Function FindIncorrect(LB As ListBox, CorrectAnswers As List(Of String)) As List(Of String)
        Dim Incorrect As List(Of String) = New List(Of String) 'Создаем массив для ошибочных вариантов
        For Each item As String In LB.Items 'Для каждого элемента в ЛистБоксе
            Dim flag As Boolean = True 'Флаг для проверки, найден ли элемент в списке верных ответов
            For index = 0 To CorrectAnswers.Count - 1 'Идем по массиву верных ответов
                If item = CorrectAnswers.Item(index) Then 'Если ответ правильный
                    flag = False
                    Exit For
                End If
            Next
            If flag Then 'Если элемент неверный
                Incorrect.Add(item) 'Добавляем в список неверных ответов
            End If
        Next
        Return Incorrect  'Возвращаем массив неверных ответов
    End Function
    Private Sub ListBox5_MouseDown(sender As Object, e As MouseEventArgs) Handles ListBox5.MouseDown, ListBox4.MouseDown, ListBox3.MouseDown, ListBox2.MouseDown, ListBox1.MouseDown
        Dim src As ListBox = DirectCast(sender, ListBox)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            For i As Integer = 0 To src.Items.Count - 1
                If src.GetItemRectangle(i).Contains(e.Location) Then
                    src.DoDragDrop(src, DragDropEffects.Move)
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub ListBox5_DragEnter(sender As Object, e As DragEventArgs) Handles ListBox5.DragEnter, ListBox4.DragEnter, ListBox3.DragEnter, ListBox2.DragEnter, ListBox1.DragEnter
        Dim src As ListBox = DirectCast(sender, ListBox)
        If e.Data.GetDataPresent("System.Windows.Forms.ListBox") Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub ListBox5_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox5.DragDrop, ListBox4.DragDrop, ListBox3.DragDrop, ListBox2.DragDrop, ListBox1.DragDrop
        Dim src As ListBox = DirectCast(sender, ListBox)
        If e.Data.GetDataPresent("System.Windows.Forms.ListBox") Then
            Dim data As ListBox = DirectCast(e.Data.GetData("System.Windows.Forms.ListBox"), ListBox)
            Dim item As Object = data.SelectedItem
            data.Items.Remove(item)
            Dim location As Point = src.PointToClient(New Point(e.X, e.Y))
            Dim dropIndex As Integer = -1
            For i As Integer = 0 To src.Items.Count - 1
                If src.GetItemRectangle(i).Contains(location) Then
                    dropIndex = i
                    Exit For
                End If
            Next
            If dropIndex = -1 Then
                src.Items.Add(item)
            Else
                src.Items.Insert(dropIndex, item)
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MessageBox.Show("Чтобы распределить налоги по категориям, перетащите их из списка в окна", "Подсказка по использованию")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MessageBox.Show("1. В каждом окне по 4 налога
2. Читайте внимательно ключевые слова в названии налогов", "Подсказка по заданию")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim CorrectAnswers As List(Of String) = New List(Of String) 'Массив корректных ответов
        Dim Result As List(Of String) 'Массив для некорректных ответов
        Dim CorrectCounter As Integer = 0 'счетчик для корректности всех ЛистБоксов
        'Физическим лицам
        CorrectAnswers.Add("На доход физического лица") 'Добавление правильных ответов в массив
        CorrectAnswers.Add("Взнос в пенсионный фонд")
        CorrectAnswers.Add("НДС в цене любого товара")
        CorrectAnswers.Add("Взнос в фонд любого товара")
        Result = FindIncorrect(ListBox2, CorrectAnswers) 'Используем функцию для проверки
        If Result.Count <> 0 Then 'если массив с неверными ответами не пустой
            For index = 0 To Result.Count - 1
                ListBox2.Items.Remove(Result.Item(index)) 'Удаляем из текущего списка
                ListBox1.Items.Add(Result.Item(index)) 'Добавляем в изначальный
            Next
        Else CorrectCounter += 1
        End If  'Для остальных списков алгоритм идентичный

        'Юридическим лицам
        CorrectAnswers.Clear()
        CorrectAnswers.Add("Налог на прибыль")
        CorrectAnswers.Add("Земельный налог")
        CorrectAnswers.Add("Налог на имущество юридических лиц")
        CorrectAnswers.Add("Транспортный налог")
        Result = FindIncorrect(ListBox3, CorrectAnswers)
        If Result.Count <> 0 Then
            For index = 0 To Result.Count - 1
                ListBox3.Items.Remove(Result.Item(index))
                ListBox1.Items.Add(Result.Item(index))
            Next
        Else CorrectCounter += 1
        End If
        'Индивидуальным предпринимателям
        CorrectAnswers.Clear()
        CorrectAnswers.Add("Налог на доходы работников")
        CorrectAnswers.Add("НДС")
        CorrectAnswers.Add("Уплата стоимости патента")
        CorrectAnswers.Add("Налог на временный доход")
        Result = FindIncorrect(ListBox4, CorrectAnswers)
        If Result.Count <> 0 Then
            For index = 0 To Result.Count - 1
                ListBox4.Items.Remove(Result.Item(index))
                ListBox1.Items.Add(Result.Item(index))
            Next
        Else CorrectCounter += 1
        End If
        'Налоговые вычеты
        CorrectAnswers.Clear()
        CorrectAnswers.Add("Вычет на обучение")
        CorrectAnswers.Add("Вычет на жилье")
        CorrectAnswers.Add("Вычет на лечение")
        CorrectAnswers.Add("Вычет по ипотеке")
        Result = FindIncorrect(ListBox5, CorrectAnswers)
        If Result.Count <> 0 Then
            For index = 0 To Result.Count - 1
                ListBox5.Items.Remove(Result.Item(index))
                ListBox1.Items.Add(Result.Item(index))
            Next
        Else CorrectCounter += 1
        End If

        If CorrectCounter = 4 Then
            If ListBox1.Items.Count = 0 Then
                MessageBox.Show("Все верно!", "Поздравление")
            Else MessageBox.Show("Вы движетесь в верном направлении, но распределите элементы до конца!", "Предупреждение")
            End If
        Else
            MessageBox.Show("Неверно. Попробуйте снова :( 
Неккоректные варианты переместились назад в список", "Предупреждение")
        End If
    End Sub
End Class

Public Class MyTasker


    Private Sub MyTasker_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        InicioNotes.Show()
        Me.Dispose()
    End Sub

    Private Sub MyTasker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Adds new node as a child node of the currently selected node.
        Dim newNode As TreeNode = New TreeNode(InputBox("Escribe el Texto que sera Agregado [PADRE]"))
        TreeView1.Nodes.Add(newNode)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Adds new node as a child node of the currently selected node.
        Dim newNode As TreeNode = New TreeNode(InputBox("Escribe el Texto que sera Agregado [HIJO]"))
        TreeView1.SelectedNode.Nodes.Add(newNode)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Removes currently selected node, or root if nothing is selected.
        TreeView1.Nodes.Remove(TreeView1.SelectedNode)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ' Clears all nodes.
        TreeView1.Nodes.Clear()
    End Sub

    Private Sub AgregarTareaGeneralToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgregarTareaGeneralToolStripMenuItem.Click
        ' Adds new node as a child node of the currently selected node.
        Dim newNode As TreeNode = New TreeNode(InputBox("Escribe el Texto que sera Agregado [PADRE]"))
        TreeView1.Nodes.Add(newNode)
    End Sub

    Private Sub AgregarTareaEspecificaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgregarTareaEspecificaToolStripMenuItem.Click
        ' Adds new node as a child node of the currently selected node.
        Dim newNode As TreeNode = New TreeNode(InputBox("Escribe el Texto que sera Agregado [HIJO]"))
        TreeView1.SelectedNode.Nodes.Add(newNode)
    End Sub

    Private Sub BorrarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarToolStripMenuItem.Click
        ' Removes currently selected node, or root if nothing is selected.
        TreeView1.Nodes.Remove(TreeView1.SelectedNode)
    End Sub

    Private Sub BorrarTodoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarTodoToolStripMenuItem.Click
        ' Clears all nodes.
        TreeView1.Nodes.Clear()
    End Sub

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        End
    End Sub

    Private Sub ActivarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivarToolStripMenuItem.Click
        Me.TopMost = True
    End Sub

    Private Sub DesactivarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesactivarToolStripMenuItem.Click
        Me.TopMost = False
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If Panel2.Visible = False Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        CheckedListBox1.Items.Add(InputBox("Escribe el Texto que sera Agregado [Tarea]"))
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        CheckedListBox1.Items.Remove(CheckedListBox1.SelectedItem)
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        CheckedListBox1.Items.Add(InputBox("Escribe el Texto que sera Agregado [Tarea]"))
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        CheckedListBox1.Items.Remove(CheckedListBox1.SelectedItem)
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        CheckedListBox1.Items.Clear()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        CheckedListBox1.Items.Clear()
    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        End
    End Sub

    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        Me.TopMost = True
    End Sub

    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        Me.TopMost = False
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        About.Show()
    End Sub
End Class

import wx


class MainFrame(wx.Frame):
    def __init__(self):
        super().__init__(parent=None, title='Hello World')

        panel = wx.Panel(self)
        self.text_ctrl = wx.TextCtrl(panel)
        my_btn = wx.Button(panel, label='Press Me please hit me')
        my_btn.Bind(wx.EVT_BUTTON, self.on_press)
        self.my_lbl = wx.StaticText(panel)

        my_sizer = wx.BoxSizer(wx.VERTICAL)
        my_sizer.Add(self.text_ctrl, 0, wx.ALL | wx.LEFT, 5)
        my_sizer.Add(my_btn, 0, wx.ALL | wx.CENTER, 5)
        my_sizer.Add(self.my_lbl, 0, wx.ALL | wx.CENTER, 5)
        panel.SetSizer(my_sizer)

        self.Show()

    def on_press(self, event):
        value = self.text_ctrl.GetValue()

        self.my_lbl.SetLabel(value)


app = wx.App()
frame = MainFrame()
app.MainLoop()

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

class ImageGame:Form
{
  private MainMenu ImageMenu;
  private MenuItem FileMenu;
  private MenuItem FileOpen;
  private MenuItem FileExit;
  private Bitmap ImageFile;
  private int ImageSize;
  private int SmallImageSize;
  private const int PIECECOUNT=3;
  private PictureBox[] Viewer=new PictureBox[PIECECOUNT*PIECECOUNT];
  private int OffSetX;
  private int OffSetY;
  private Rectangle ImageRect;
  private Button Shuffle;
  private Button MoveLeft;
  private Button MoveRight;
  private Button MoveTop;
  private Button MoveBottom;
  private Button Reset;
  private int EmptyElementIndex=PIECECOUNT*PIECECOUNT-1;
  private ToolTip HelpText=new ToolTip();

  public ImageGame()
  {
    FileMenu=new MenuItem();
    FileMenu.Text="&File";

    FileOpen=new MenuItem();
    FileOpen.Text="&Open...";
    FileOpen.Click+=new EventHandler(Menu_Click);

    FileExit=new MenuItem();
    FileExit.Text="E&xit";
    FileExit.Click+=new EventHandler(Menu_Click);

    FileMenu.MenuItems.Add(FileOpen);
    FileMenu.MenuItems.Add("-");
    FileMenu.MenuItems.Add(FileExit);

    ImageMenu=new MainMenu(new MenuItem[]{FileMenu});
    this.Menu=ImageMenu;
    this.AutoScroll=true;

    Shuffle=new Button();
    Shuffle.Text="&Shuffle";
    Shuffle.Location=new Point(15,15);
    Shuffle.Enabled=false;
    Shuffle.Click+=new EventHandler(Shuffle_Click);
    this.Controls.Add(Shuffle);

    MoveTop=new Button();
    MoveTop.Text="T";
    MoveTop.Enabled=false;
    MoveTop.Location=new Point(Shuffle.Left+Shuffle.Width+50,0);
    MoveTop.Click+=new EventHandler(Direction_Click);
    MoveTop.Size=new Size(24,24);
    this.Controls.Add(MoveTop);

    MoveBottom=new Button();
    MoveBottom.Text="B";
    MoveBottom.Enabled=false;
    MoveBottom.Location=new Point(MoveTop.Left,MoveTop.Top+MoveTop.Height*2+5);
    MoveBottom.Click+=new EventHandler(Direction_Click);
    MoveBottom.Size=new Size(24,24);
    this.Controls.Add(MoveBottom);

    MoveLeft=new Button();
    MoveLeft.Text="L";
    MoveLeft.Enabled=false;
    MoveLeft.Location=new Point(MoveTop.Left-MoveTop.Width,MoveTop.Top+MoveTop.Height+3);
    MoveLeft.Click+=new EventHandler(Direction_Click);
    MoveLeft.Size=new Size(24,24);
    this.Controls.Add(MoveLeft);


    MoveRight=new Button();
    MoveRight.Text="R";
    MoveRight.Enabled=false;
    MoveRight.Location=new Point(MoveTop.Left+MoveTop.Width,MoveLeft.Top);
    MoveRight.Size=new Size(24,24);
    MoveRight.Click+=new EventHandler(Direction_Click);
    this.Controls.Add(MoveRight);

    Reset=new Button();
    Reset.Text="&Reset";
    Reset.Location=new Point(MoveRight.Left+MoveRight.Width+15,Shuffle.Top);
    Reset.Click+=new EventHandler(Reset_Click);
    Reset.Enabled=false;
    this.Controls.Add(Reset);

    OffSetX=0;
    OffSetY=75;
    
    for(int i=0;i<Viewer.Length;i++)
    {
      Viewer[i]=new PictureBox();
      this.Controls.Add(Viewer[i]);
    }
  }

  public void Menu_Click(object sender,EventArgs e)
  {
    if(sender==FileOpen)
    {
      OpenFileDialog dlgOpen=new OpenFileDialog();
      dlgOpen.RestoreDirectory=true;
      dlgOpen.Filter="Bitmap Files(*.bmp)|*.bmp|JPEG Files(*.jpg)|*.jpg|GIF Files(*.gif)|*.gif";
      dlgOpen.FilterIndex=1;
      if(dlgOpen.ShowDialog()==DialogResult.OK)
      {
          int sourceWidth = 100;
        Shuffle.Enabled=true;
        Reset.Enabled=true;
        ImageFile=new Bitmap(dlgOpen.FileName);
        ImageSize=(ImageFile.Width<ImageFile.Height)?ImageFile.Width:ImageFile.Height;
        ImageSize=ImageSize-ImageSize%PIECECOUNT;
        SmallImageSize=ImageSize/PIECECOUNT;
        Reset_Click(null,null);
      }
    }
    if(sender==FileExit)
    {
      this.Dispose();
      Application.Exit();
    }
  }

  public void Reset_Click(object sender,EventArgs e)
  {
    if(ImageFile!=null)
    {
      int ElementIndex=0;
      for(int j=0;j<PIECECOUNT;j++)
      {
        for(int i=0;i<PIECECOUNT;i++)
        {             
          try
          {
            ImageRect=new Rectangle(i*SmallImageSize,j*SmallImageSize,SmallImageSize,SmallImageSize);
            Viewer[ElementIndex].Image=null;
            if(ElementIndex!=Viewer.Length-1)                   
              Viewer[ElementIndex].Image=ImageFile.Clone(ImageRect,PixelFormat.DontCare);
            Viewer[ElementIndex].Location=new Point(OffSetX+i*SmallImageSize,OffSetY+j*SmallImageSize+1);
            Viewer[ElementIndex].Size=new Size(SmallImageSize+1,SmallImageSize+1);
            Viewer[ElementIndex].Tag=ElementIndex.ToString();
            HelpText.SetToolTip(Viewer[ElementIndex],ElementIndex.ToString());
            ElementIndex++;
            this.Invalidate();
          }
          catch(Exception)
          {              
          }
        }
      }
    }
    Shuffle.Enabled=true;
    EmptyElementIndex=PIECECOUNT*PIECECOUNT-1;
  }


  public void Shuffle_Click(object sender,EventArgs e)
  {
    MoveLeft.Enabled=true;
    MoveRight.Enabled=true;
    MoveTop.Enabled=true;
    MoveBottom.Enabled=true;
    Shuffle.Enabled=false;
    Reset.Enabled=true;
    Bitmap temp;
    string temptag;
  
    for(int j=0;j<PIECECOUNT;j++)
    {
      for(int i=j+1;i<PIECECOUNT;i++)
      {             
        try
        {
          temp=(Bitmap)Viewer[j*PIECECOUNT+i].Image;
          temptag=Viewer[j*PIECECOUNT+i].Tag.ToString();
          Viewer[j*PIECECOUNT+i].Image=Viewer[i*PIECECOUNT+j].Image;
          Viewer[j*PIECECOUNT+i].Tag=Viewer[i*PIECECOUNT+j].Tag;
          HelpText.SetToolTip(Viewer[j*PIECECOUNT+i],Viewer[j*PIECECOUNT+i].Tag.ToString());
          Viewer[i*PIECECOUNT+j].Image=temp;
          Viewer[i*PIECECOUNT+j].Tag=temptag;
          HelpText.SetToolTip(Viewer[i*PIECECOUNT+j],Viewer[i*PIECECOUNT+j].Tag.ToString());  
          this.Invalidate();
        }
        catch(Exception)
        {              
        }
      }
    }
  }

  public void Direction_Click(object sender,EventArgs e)
  {
    Bitmap temp;
    string temptag;
    int IndexToSwap=-1;

    if(sender==MoveLeft)
    {
      if(IsMoveValid("L"))
      {
        IndexToSwap=EmptyElementIndex-1;
      }
    }
    if(sender==MoveRight)
    {
      if(IsMoveValid("R"))
      {
        IndexToSwap=EmptyElementIndex+1;
      }
    }
    if(sender==MoveTop)
    {
      if(IsMoveValid("T"))
      {
        IndexToSwap=EmptyElementIndex-PIECECOUNT;
      }
    }
    if(sender==MoveBottom)
    {
      if(IsMoveValid("B"))
      {
        IndexToSwap=EmptyElementIndex+PIECECOUNT;
      }
    }
    if(IndexToSwap!=-1)
    {
      try
      {
        temp=(Bitmap)Viewer[IndexToSwap].Image;
        temptag=Viewer[IndexToSwap].Tag.ToString();
        Viewer[IndexToSwap].Image=Viewer[EmptyElementIndex].Image;
        Viewer[IndexToSwap].Tag=Viewer[EmptyElementIndex].Tag;
        HelpText.SetToolTip(Viewer[IndexToSwap],Viewer[IndexToSwap].Tag.ToString());
        Viewer[EmptyElementIndex].Image=temp;
        Viewer[EmptyElementIndex].Tag=temptag;
        HelpText.SetToolTip(Viewer[EmptyElementIndex],Viewer[EmptyElementIndex].Tag.ToString());
        EmptyElementIndex=IndexToSwap;
        this.Invalidate();
      }
      catch(Exception)
      {
      }
    }
    if(IsWon)
    {
      MessageBox.Show("You Won the Game....");
    }
  }

  public bool IsMoveValid(string Direction)
  {
    bool IsValid=false;
    switch(Direction)
    {
      case "L":
        IsValid=(EmptyElementIndex%PIECECOUNT!=0);
        break;
      case "R":
        IsValid=(EmptyElementIndex%PIECECOUNT!=(PIECECOUNT-1));
        break;
      case "T":
        IsValid=(EmptyElementIndex>PIECECOUNT-1);
        break;
      case "B":
        IsValid=(EmptyElementIndex<PIECECOUNT*PIECECOUNT-PIECECOUNT);
        break;
    }
    return IsValid;
  }

  public bool IsWon
  {
    get
    {
      for(int i=0;i<Viewer.Length;i++)
      {
        if(Viewer[i].Tag.ToString()!=i.ToString())
          return false;
      }
      return true;
    }
  }

  private void InitializeComponent()
  {
      this.SuspendLayout();
      // 
      // ImageGame
      // 
      this.ClientSize = new System.Drawing.Size(920, 494);
      this.Name = "ImageGame";
      this.Load += new System.EventHandler(this.ImageGame_Load);
      this.ResumeLayout(false);

  }

  private void ImageGame_Load(object sender, EventArgs e)
  {

  }

}


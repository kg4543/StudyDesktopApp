# TreeNode

<kbd>![TreeNode](/Capture/WinForm/TreeNode.PNG "트리노드")</kbd>

## 1. TreeNode 만들기

- TreeNode를 활용하여 생성 (using System.Drawing;)
- TreeNode.Nodes.Add();를 활용하여 Node에 Item 추가
- TreeNode.Nodes.Add(TreeNode);를 활용하여 Node를 활장
```
private void FrmMain_Load(object sender, EventArgs e)
        {
            TreeNode root = new TreeNode("영국 왕실");

            TreeNode stuart = new TreeNode("스튜어트 가");
            stuart.Nodes.Add("ANN1","앤(1707~1714)");

            TreeNode hanover = new TreeNode("하노버 가");
            hanover.Nodes.Add("GRG1","조지 1세(1714~1727)");
            hanover.Nodes.Add("GRG2","조지 2세(1727~1760)");
            hanover.Nodes.Add("GRG3","조지 3세(1760~1820)");
            hanover.Nodes.Add("GRG4","조지 4세(1820~1830)");
            hanover.Nodes.Add("Wilium", "윌리엄(1830~1837)");
            hanover.Nodes.Add("Victory", "빅토리아(1837~1901)");

            root.Nodes.Add(stuart);
            root.Nodes.Add(hanover);

            TrvList.Nodes.Add(root);
            TrvList.ExpandAll();
        }
```

## 2. TreeNode 선택

- 선택 객체의 속성을 활용하여 선택값 판별 (e.Node.Name)
```
private void TrvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // MessageBox.Show(e.Node.ToString());
            PcbPhoto.Image = null;
            TxtDescription.Text = string.Empty;

            if (e.Node.Name == "ANN1")
            {
                PcbPhoto.Image = Bitmap.FromFile("./Images/Anne.jpg");
                TxtDescription.Text = "앤 여왕은 1702년 3월8일 잉글랜드와 스코틀랜드에서";
            }
            else if (e.Node.Name == "GRG1")
            {
                PcbPhoto.Image = Bitmap.FromFile("./Images/King_George_I.jpg");
                TxtDescription.Text = "조지 1세는 잉글랜드와 스코틀랜드에서";
            }
        }
```

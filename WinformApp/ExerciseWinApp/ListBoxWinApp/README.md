# ListBox

<kbd>![](/Capture/WinForm/리스트박스.PNG "리스트 박스")</kbd>

## 1. List 작성

- 디자인 상에서 '항목편집' 기능을 활용하여 리스트 편집
- Listview.Items.Add(); 를 활용하여 리스트 항목 추가
- List<string>를 활용하여 리스트 배열 생성

```
private void FrmMain_Load(object sender, EventArgs e)
        {
            // 살기좋은 도시 초기화
            LsbGoodCity.Items.Add("오스트리아, 빈");
            LsbGoodCity.Items.Add("호주, 멜버른");
            LsbGoodCity.Items.Add("일본, 오사카");
            LsbGoodCity.Items.Add("캐나다, 캘거리");
            LsbGoodCity.Items.Add("호주, 시드니");
            LsbGoodCity.Items.Add("캐나다, 밴쿠버");
            LsbGoodCity.Items.Add("캐나다, 토론토");
            LsbGoodCity.Items.Add("덴마크, 코페하겐");
            LsbGoodCity.Items.Add("호주, 애들레이드");
            LsbGoodCity.Items.Add("일본, 도쿄");

            List<string> lstCountry = new List<string>()
            {
                "미국","러시아","중국","영국","독일","프랑스","일본","이스라엘","사우디아라비아"
            };
            LsbHappyCountry.DataSource = lstCountry;
        }
```

## 2. List Item 선택

- 선택한 object를 ListBox 형태로 받아서 저장
- 저장한 리스트에서 선택된 값의 index 및 값을 표기
```
private void LsbGDPCountry_SelectedIndexChanged(object sender, EventArgs e) 
        {
            // 선택된 값 확인
            //MessageBox.Show(sender.ToString());
            var selItem = sender as ListBox; // ( ) 값 형식 변환 / as 참조 형식 변환
            // MessageBox.Show($"{selItem.SelectedIndex} : {selItem.SelectedItem}");
            TxtGDPIndex.Text = selItem.SelectedIndex.ToString();
            //TxtGDPItem.Text = selItem.SelectedItem.ToString();
            TxtGDPItem.Text = selItem.SelectedItem == null ? string.Empty : selItem.SelectedItem.ToString();
        }
```

- 선택한 값의 index를 활용하여 값 초기화시킴

```
private void BtnFinish_Click(object sender, EventArgs e)
        {
            LsbGDPCountry.SelectedIndex =
            LsbGoodCity.SelectedIndex =
            LsbHappyCountry.SelectedIndex = -1;
        }
```

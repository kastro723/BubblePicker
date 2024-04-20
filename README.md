# BubblePicker

    [패치 노트]

            v1.1.0 - 24.04.20
                   - Text 정렬 및 선택 기능 추가 (Text Sort)
                   - Fit 버튼을 통해 Text 위치를 image(bubble) 안으로 이동되게 수정
                   
            
            v1.0.0 - 24.04.19
                   - Bubble Picker 기능 구현

-------------------------------------------------------------------------------------

![image](https://github.com/kastro723/BubblePicker/assets/55536937/de475fde-4112-431f-9562-b1786487565e)![image](https://github.com/kastro723/BubblePicker/assets/55536937/20131b39-70c3-4fd2-a147-20f8cb87e283)
![image](https://github.com/kastro723/BubblePicker/assets/55536937/eeaf62ec-32d7-4839-b50b-181d5cad9d52)





    [기능설명]
    
            지정된 Picker의 위치에 따라 Bubble(image)과 Text의 크기와 위치를 조정하는 다양한 기능을 제공

            Inspector의 Fit 버튼 및 fit 메서드를 통한 동적 조정
            Text의 패딩 및 간격 설정
            Bubble(Image)의 확장 유형(MoveType)을 지원하여, Text의 크기 및 길이에 따른 Bubble의 확장 방향 설정
            Bubble(Image) 내에서 Text의 정렬 유형(TextSort)를 제공하여 Left, Center, Right 방향으로 Text의 위치를 유연하게 조정


    [옵션설명]
    
            Text - UI TexT(TextMeshPro)
            Image - Bubble Sprite
            Picker - Picker Sprit
            Picker Offset Y - Image와 Picker가 겹치는 간격
            Image Width - Image의 가로 길이
            Horizontal Padding - Text의 좌, 우 padding
            Vertical Padding - Text의 상, 하 padding
            Paragraph Spacing - Text의 줄 간격
            Use If Empty Text Default Size - Text가 없을 시 Image의 크기 (가로(x),  세로(y))
            MoveType - Text의 크기 및 길이에 따른 Image의 확장 방향 (Up, Down)
            TextSort - Text의 정렬 방향 (Left, Center, Right)
            Fit - Fit 동작(메서드) 실행 
                (Picker에 따른 Image와 Text 위치 갱신, 설정된 수치로 Image 및 Text 수정 및 갱신)
            

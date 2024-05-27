# BubblePicker

    [패치 노트]
    	    v2.0.2 - 24.05.28
                   - Bg와 Picker가 이동하는 기준 PickerTrans로 설정
                   - PickerTrans의 좌표 설정 및 이동으로 Bubble과 Picker 이동 가능하게 수정
		   
            v2.0.2 - 24.04.25
                   - 코드 버그 수정
                   - Font Size 설정 추가
                   - Script 부착 위치 최상위(BubblePicker)로 변경
                   
            v2.0.0 - 24.04.24
                   - World Position을 통해 정확한 위치 및 배치 구현을 위한 전체적인 코드 수정
                   - Image -> Bg 이름 수정
                   
            v1.1.0 - 24.04.20
                   - Image 내의 Text Box 정렬 및 선택 기능 추가 (Text Sort)
                   - Fit 버튼을 통해 Text 위치를 image(bubble) 안으로 이동되게 수정
                   
            
            v1.0.0 - 24.04.19
                   - Bubble Picker 기능 구현

-------------------------------------------------------------------------------------

![image](https://github.com/kastro723/BubblePicker/assets/55536937/de475fde-4112-431f-9562-b1786487565e)![image](https://github.com/kastro723/BubblePicker/assets/55536937/20131b39-70c3-4fd2-a147-20f8cb87e283)

![image](https://github.com/kastro723/BubblePicker/assets/55536937/342e101f-f26c-4195-918e-e8cd97362114)






    [기능설명]
    
            지정된 Picker의 위치에 따라 Bubble(Bg)과 Text의 크기와 위치를 조정하는 다양한 기능을 제공

            션 설정
            Bubble(Bg)의 확장 유형(MoveType)을 지원하여, Text의 크기 및 길이에 따른 Bubble의 확장 방향 설정
            Bubble(Bg) 내에서 Text Box의 정렬 유형(TextSort)을 설정
                (Left, Center, Right 방향으로 Bg 내에서 Text Box의 위치를 조정 가능)


    [옵션설명]
    
            Bg Fitter - Bg Content Size Fitter
            Text - UI Text(TextMeshPro)
            Bg - Bubble Sprite
            Picker - Picker Sprite
            Picker Offset Y - Bg와 Picker가 겹치는 간격
            Bg Width - Image의 가로 길이
            Text Width - Text box의 가로 길이
            Text Font Size - Text의 폰트 크기
            Horizontal Padding - Text의 좌, 우 padding
            Vertical Padding - Text의 상, 하 padding
            Paragraph Spacing - Text의 엔터 간 간격
            Line Spacing - Text의 줄 간 간격
            Use If Empty Text Default Size - Text가 없을 시 Image의 크기 (가로(x),  세로(y))
            MoveType - Text의 크기 및 길이에 따른 Bg의 확장 방향 (Up, Down) 
            TextSort - Text의 정렬 방향 (Left, Center, Right)
            Fit - Fit 동작(메서드) 실행 
                (Picker에 따른 Bg와 Text 위치 갱신, 설정된 수치로 Image 및 Text 수정 및 갱신)
            

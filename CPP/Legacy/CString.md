# CString
Windows 시스템을 위한 String 클래스


## Concept
- C++ MFC의 문자열 Class

- Windows와 MFC는 TCHAR/wchar_t 기반 문자열 사용
  - wchar_t: C++의 문자 타입으로 wide character(2 byte) 라는 뜻
    - 영어는 8bit 만으로 모든 문자를 Cover 가능하지만, 한글만 하더라도 8bit로는 한계가 있다
    - 따라서 2byte char를 써서 세계의 모든 문자를 cover
  - TCHAR은 매크로
    - ANSI에선 char, Unicode에선 wchar_t
    - 요즘 Windows에서는 TCHAR = wchar_t

- CString은 자동으로 LPCTSTR로 변환
    - LPCTSTR = const TCHAR*
    - 따라서 CString과 Windows/MFC는 궁합이 좋다 (이게 중요)


## _T() 매크로
- 문자열 타입 자동 변환 매크로이다

  ```cpp
  CString str = _T("Hello");
  ```


## CString 주요 함수
- Format
  ```cpp
  CString str;
  str.Foramt(_T("value = %d"), 10);
  ```

- Append
  ```cpp
  str += _T(" world");
  ```

- Compare
  ```cpp
  str.Compare(_T("Hello"));
  ```

- 길이
  ```cpp
  str.GetLength();
  ```

- substring
  ```cpp
  str.Left(3);
  str.Right(2);
  str.Mid(1,3);
  ```

- CString -> String
  ```cpp
  std::string s = "hello";
  CString cs(s.c_str());
  // In Unicode env
  ```


# std::String

## Concept
- C++의 STL 중 하나
- Char 기반으로 문자열을 생성 및 관리해주는 아주 좋은 클래스
- 다만 Char 기반이므로 MFC에선 안 먹을 가능성이 높다

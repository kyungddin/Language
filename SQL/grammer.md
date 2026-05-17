# 평균 SELECT
SELECT AVG(COLUMEN_NAME)


# 반올림
ROUND(TARGET, 자릿수)


# 컬럼 조건 필터링
- WHERE COLUMN_NAME = VALUE
- WHERE 조건 여러 개 하려면 AND/OR 사용


# 오름차순 / 내림차순
- ORDER BY COLUMN_NAME ASC/DESC
- 조건 여러개면 쉼표로 구분


# 테이블의 컬럼에 대한 지칭
- TABLE.COLUMN
- 여러 테이블이 같은 COLUMN을 쓰는 경우 ambiguous 문제 Solve 가능


# 외래 키 테이블 이용하기
JOIN 외래테이블
ON 기본테이블.COL = 외래테이블.COL

- 어떤 ROW 끼리 연결해줄지를 알려주는 작업


# DATETIME에서 연월일시분초 추출
YEAR(DATE)
MONTH(DATE)
DAY(DATE)
HOUR(DATE)
MINUTE(DATE)
SECOND(DATE)


# NULL 채우기
IFNULL(TLNO, 'NONE') AS TLNO


# NULL 필터링
WHERE TLNO is not NULL


# Alias && DATE_FORMAT
SELECT b.TITLE,
       b.BOARD_ID,
       r.REPLY_ID,
       r.WRITER_ID,
       r.CONTENTS,
       DATE_FORMAT(r.CREATED_DATE, '%Y-%m-%d') AS CREATED_DATE
FROM USED_GOODS_BOARD b
JOIN USED_GOODS_REPLY r
ON b.BOARD_ID = r.BOARD_ID
WHERE YEAR(b.CREATED_DATE) = 2022
AND MONTH(b.CREATED_DATE) = 10
ORDER BY r.CREATED_DATE ASC, b.TITLE ASC;


# SQL 와일드카드
- % : ALL
- _ : 문자 하나
- [] : 해당 위치에 괄호 안의 문자가 있을 경우


# GROUP BY 
- GROUP BY만 단독으로 쓰일 경우 중복 제거된 조합을 보여준다 (유니크 테이블..)

- GROUP BY를 SELECT/HAVING절의 조건식과 함께 써주면 비로소 효과를 발휘함
    - AVG를 SELECT에서 쓸 때 GROUP BY를 해주지 않으면 모든 ROW에 대해 평균을 내버린다
    - 따라서 GROUP BY로 특정 colume 값에 대해 묶어줄 필요가 있다

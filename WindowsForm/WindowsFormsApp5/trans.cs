using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{

    class trans
    {
        public static string chosung = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        public static string jungsung = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        public static string jongsung = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
        public static ushort UnicodeHangeulBase = 0xAC00;
        public static ushort UnicodeHangeulLast = 0xD79F;

        public static int length;//입력받은 자음모음길이
        public static string line;//입력받는 자음모음
        public static int choice = 0; //모음의 갯수를 저장
        public static int c = 0;// 이중자음할떄 저장되있는 값 이중자음의 자릿수값을 말하는거다

        public static string totalnumber;


        //****************************************************************************************
        private static int[] sign = { 12, 2111, 2121, 211, 1, 1121, 221, 1111, 11, 1222, 212, 1211, 22, 21, 222, 1221, 2212, 121, 111, 2, 112, 1112, 122, 2112, 2122, 2211, 22222, 12222, 11222, 11122, 11112, 11111, 21111, 22111, 22211, 22221, 4 };
        private static char[] en_c = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };
        private static char[] ko_c = { 'ㅗ', 'ㄷ', 'ㅊ', 'ㅡ', 'ㅏ', 'ㄴ', 'ㅅ', 'ㅜ', 'ㅑ', 'ㅎ', 'ㅇ', 'ㄱ', 'ㅁ', 'ㅛ', 'ㅍ', 'ㅈ', 'ㅔ', 'ㅠ', 'ㅕ', 'ㅓ', 'ㅣ', 'ㄹ', 'ㅂ', 'ㅋ', 'ㅐ', 'ㅌ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };

        public static char signToko(int a)  //모스부호를 한글로 변환
        {
            int i;
            int c = -1;
            for (i = 0; i < 36; i++)
            {
                if (sign[i] == a)
                    c = i;
            }
            if (c == -1)
                return ' ';
            return ko_c[c];
        }
        public static char signToen(int a)  //모스부호를 영어로 변환
        {
            int i;
            int c = -1;
            for (i = 0; i < 36; i++)
            {
                if (sign[i] == a)
                    c = i;
            }
            if (c == -1)
                return ' ';
            return en_c[c];
        }







        public static int koTosign(char a)
        {

            int i = 0;

            for (i = 0; i < 37; i++)
            {
                if (a == en_c[i])
                {
                    totalnumber = totalnumber + sign[i] + '3';
                    return sign[i];
                }
                else { }
            }
            for (i = 0; i < 37; i++)
            {
                if (a == ko_c[i])
                {
                    totalnumber = totalnumber + sign[i] + '3';
                    return sign[i];

                }
                else { }
            }
            return 0;

        }







        //edf,.jmhdsjkh,fhdskjfhdjkfhdkfhjhdk,jdshgk,nfhgfk,dfhk,jhdffhdsk,jhdskjh jfdsk jhfjhdkh jfhdknfhdsk,hjfhsedjk,hnfhwerk,jhfjek,jfghjed,mfjedkn fnedkjfjewkjl,fjelkjgf

        public static string MergeJaso(char choSung, char jungSung, char jongSung)

        {
            int ChoSungPos, JungSungPos, JongSungPos;
            int nUniCode;

            if (jongSung == '\0')
                jongSung = ' ';

            ChoSungPos = chosung.IndexOf(choSung);     // 초성 위치
            JungSungPos = jungsung.IndexOf(jungSung);   // 중성 위치
            JongSungPos = jongsung.IndexOf(jongSung);   // 종성 위치

            // 앞서 만들어 낸 계산식
            nUniCode = UnicodeHangeulBase + (ChoSungPos * 21 + JungSungPos) * 28 + JongSungPos;
            // 코드값을 문자로 변환
            char temp = Convert.ToChar(nUniCode);
            return temp.ToString();

        }
        public static bool ja_check(char a)     //자음인지 확인
        {
            for (int i = 0; i < 19; i++)
            {
                if (a == chosung[i])
                {
                    return true;
                }
            }
            return false;
        }
        public static char Jaeum2(char a, char b) // 자음두개의집합을찾아주는거

        {

            int c = -1;



            for (int i = 0; i <= 15; i++)

            {

                if (doubleja[i, 0] == a && doubleja[i, 1] == b)

                {
                    c = i;

                }

            }
            if (c == -1)
                return ' ';
            else
                return ja[c];
            //문제찾음



        }
        public static bool mo_check(char a)     //모음인지 확인
        {
            for (int i = 0; i < 21; i++)
            {
                if (a == jungsung[i])
                {
                    return true;
                }
            }
            return false;
        }
        private static char[] ja1 =    //이중자음체크

        {

        'ㄲ','ㄸ','ㅃ','ㅆ','ㅉ'

        };

        private static char[,] doubleja1 =   //이중자음

    {

        { 'ㄱ','ㄱ'}, { 'ㄷ','ㄷ'},{ 'ㅂ','ㅂ'},{ 'ㅅ','ㅅ'},{ 'ㅈ','ㅈ'}, //까 따 빠 싸 짜 
    };

        public static char Jaeum1(char a, char b) // 자음두개의집합을찾아주는거

        {

            int c = -1;



            for (int i = 0; i <= 4; i++)

            {

                if (doubleja1[i, 0] == a && doubleja1[i, 1] == b)

                {
                    c = i;

                }

            }
            if (c == -1)
                return ' ';
            else
                return ja1[c];
            //문제찾음



        }
        public static char Moeum(char a, char b) //모음두개의집합을찾아주는거

        {

            int c = -1;

            for (int i = 0; i <= 8; i++)

            {

                if (doublema[i, 0] == a && doublema[i, 1] == b)

                {

                    c = i;

                }

            }


            if (c == -1)
                return ' ';

            else
                return mo[c];

        }

        public static void Word_reset(char[] a)
        {
            a[0] = ' ';
            a[1] = ' ';
            a[2] = ' ';
        }
























        private static char[] ja =    //이중자음체크
        {
        'ㄲ','ㄸ','ㅃ','ㅆ','ㅉ','ㄳ','ㄵ','ㄶ','ㄺ','ㄻ','ㄼ','ㄽ','ㄾ','ㄿ','ㅀ','ㅄ'
        };

        private static char[,] doubleja =   //이중자음
    {
        { 'ㄱ','ㄱ'}, { 'ㄷ','ㄷ'},{ 'ㅂ','ㅂ'},{ 'ㅅ','ㅅ'},{ 'ㅈ','ㅈ'}, //까 따 빠 싸 짜 
        { 'ㄱ','ㅅ'}, { 'ㄴ','ㅈ'}, { 'ㄴ','ㅎ'}, { 'ㄹ','ㄱ'},// ㄳ ㄵ ㄶ ㄺ 
        { 'ㄹ','ㅁ'}, { 'ㄹ','ㅂ'}, { 'ㄹ','ㅅ'},//ㄻ ㄼ ㄽ  
        { 'ㄹ','ㅌ'}, { 'ㄹ','ㅍ'}, { 'ㄹ','ㅎ'}, { 'ㅂ','ㅅ'}// ㄾ ,ㄿ,ㅀ ㅄ
    };

        //******************************************************************************
        private static char[] mo =
               {
          'ㅒ','ㅖ','ㅘ','ㅙ','ㅚ','ㅝ','ㅞ','ㅟ','ㅢ'
        };
        private static char[,] doublema =   //이중모음
        {
             { 'ㅑ','ㅣ'}, { 'ㅕ','ㅣ'}, { 'ㅗ','ㅏ'},
              { 'ㅗ','ㅐ'}, { 'ㅗ','ㅣ'}, { 'ㅜ','ㅓ'},
               { 'ㅜ','ㅔ'}, { 'ㅜ','ㅣ'}, { 'ㅡ','ㅣ'}
        };
        //******************************************************************************
        static void Divide(char c)
        {
            ushort check = Convert.ToUInt16(c);
            /*
            * 합칠 때 제일 처음에 UnicodeHangeulBase 를 더해 줬으므로
            * 제일 먼저 빼줘야 합니다.
            */



            if (c == ' ')
            {
                koTosign(c);
                //  Console.Write(koTosign(c));
                return;

            }
            else if (c >= '0' && c <= '9')
            {
                koTosign(c);
                // Console.Write(koTosign(c));                                         
                return;
            }
            else if (c >= 'a' && c <= 'z')
            {
                koTosign(c);
                //  Console.Write(koTosign(c));
                return;
            }
            else if (c >= 'ㄱ' && 'ㅎ' >= c)
            {
                int num = -1;
                for (int i = 0; i < 16; i++)
                {
                    if (c == ja[i])
                        num = i;
                }
                if (num == -1)
                {
                    koTosign(c);
                    //Console.Write(koTosign(c));
                    return;
                }
                else
                {

                    koTosign(doubleja[num, 0]);
                    koTosign(doubleja[num, 1]);
                    //   Console.Write(koTosign(doubleja[num, 0]));
                    //  Console.Write(koTosign(doubleja[num, 1]));
                    return;
                }

            }
            //이위에는 오직 자음만 나왔을때 체크하는거

            else if (c >= 'ㅏ' && c <= 'ㅣ')
            {
                int num = -1;
                for (int i = 0; i < 9; i++)
                {
                    if (c == mo[i])
                        num = i;
                }
                if (num == -1)
                {
                    koTosign(c);
                    //Console.Write(koTosign(c));
                    return;
                }
                else
                {
                    koTosign(doublema[num, 0]);
                    koTosign(doublema[num, 1]);

                    //Console.Write(koTosign(doublema[num, 0]));
                    //Console.Write(koTosign(doublema[num, 1]));
                    return;
                }
            }
            //이거 위에는 모음만받았을때 분리하는작업

            int Code = check - UnicodeHangeulBase;

            int JongsungCode = Code % 28; // 종성 코드 분리
            Code = (Code - JongsungCode) / 28;

            int JungsungCode = Code % 21; // 중성 코드 분리
            Code = (Code - JungsungCode) / 21;

            int ChosungCode = Code; // 남는 게 자동으로 초성이 됩니다.

            char Chosung = chosung[ChosungCode]; // Chosung 목록 중에서 ChosungCode 번째 있는 글자
            char Jungsung = jungsung[JungsungCode];
            char Jongsung = jongsung[JongsungCode];
            for (int i = 0; i < 16; i++)
            {
                if (Chosung == ja[i])
                {
                    koTosign(doubleja[i, 0]);
                    koTosign(doubleja[i, 1]);
                    // Console.Write(koTosign(doubleja[i, 0]));
                    //   Console.Write(koTosign(doubleja[i, 1]));
                    break;
                }
                else if (i == 15)
                    koTosign(Chosung);
                //  Console.Write(koTosign(Chosung));
            }
            for (int i = 0; i < 9; i++)
            {
                if (Jungsung == mo[i])
                {
                    koTosign(doublema[i, 0]);
                    koTosign(doublema[i, 1]);

                    //  Console.Write(koTosign(doublema[i, 0]));
                    //     Console.Write(koTosign(doublema[i, 1]));
                    break;
                }
                else if (i == 8)
                    koTosign(Jungsung);
                //    Console.Write(koTosign(Jungsung));
            }

            if (Jongsung == ' ')
            {
                return;
            }
            for (int i = 0; i < 16; i++)
            {
                if (Jongsung == ja[i])
                {

                    koTosign(doubleja[i, 0]);
                    koTosign(doubleja[i, 1]);

                    //   Console.Write(koTosign(doubleja[i, 0]));
                    //    Console.Write(koTosign(doubleja[i, 1]));
                    break;
                }
                else if (i == 15)
                    koTosign(Jongsung);
                // Console.Write(koTosign(Jongsung));

            }
        }
        public static string Ko_div(string line)
        {
            int length = line.Length;
            for (int i = 0; i < length; i++)
            {
                Divide(line[i]);

            }

            totalnumber = totalnumber.Remove(totalnumber.Length - 1, 1);
            return totalnumber;
        }
        public static string[] Ko_mul(string GetAD)
        {
            string[] words = GetAD.Split('3');
            string S;
            string S_en;
            S = "";
            S_en = "";
            foreach (string w in words)
            {
                S += signToko(Convert.ToInt32(w));
                S_en += signToen(Convert.ToInt32(w));
            }
            int round;
            char buffer;
            char buffer2; //종성일때 합쳐지기전 버퍼
            char[] word = new char[3];
            string outputS;
            outputS = "";
            S = S + '\0';
            round = 1;
            for (int i = 0; i < S.Length - 1; i++)
            {
                buffer = S[i];
                if (round == 1)     //초성
                {
                    if (ja_check(buffer))   //자음일 때
                    {
                        if (ja_check(S[i + 1])) //다음 단어가 자음일 때
                        {
                            if (Jaeum1(buffer, S[i + 1]) == ' ')
                            { //합쳐지지 않는 단어일 때
                                outputS += buffer;
                                Word_reset(word);
                                round = 1;
                            }
                            else                                //합쳐지는 단어일 때
                            {
                                buffer = Jaeum1(buffer, S[i + 1]);      //합친다
                                i++;
                                if (ja_check(S[i + 1])) //합친 다음 단어가 자음일 때
                                {
                                    outputS += buffer;
                                    Word_reset(word);
                                    round = 1;
                                }
                                else if (mo_check(S[i + 1]))    //합친 다음 단어가 모음일 때
                                {
                                    word[0] = buffer;
                                    round = 2;
                                }
                                else                        //나머지
                                {
                                    outputS += buffer;
                                    Word_reset(word);
                                    round = 1;
                                }
                            }
                        }
                        else if (mo_check(S[i + 1]))    //다음 단어가 모음일 때
                        {
                            word[0] = buffer;
                            round = 2;
                        }
                        else
                        {
                            outputS += buffer;
                            Word_reset(word);
                            round = 1;
                        }
                    }
                    else if (mo_check(buffer))  //모음일 때
                    {
                        outputS += buffer;
                        Word_reset(word);
                        round = 1;
                    }
                    else                        //나머지
                    {
                        outputS += buffer;
                        Word_reset(word);
                        round = 1;
                    }
                }
                else if (round == 2)        //종성
                {
                    if (mo_check(S[i + 1]))     //다음 단어가 모음일 때
                    {
                        if (Moeum(buffer, S[i + 1]) == ' ')     //합쳐지지 않는 단어일 때
                        {
                            word[1] = buffer;
                            outputS += MergeJaso(word[0], word[1], word[2]);
                            Word_reset(word);
                            round = 1;
                        }
                        else                                    //합쳐지는 단어일 때
                        {
                            buffer = Moeum(buffer, S[i + 1]);
                            i++;
                            if (mo_check(S[i + 1]))     //합쳐진 다음 단어가 모음일 때
                            {
                                word[1] = buffer;
                                outputS += MergeJaso(word[0], word[1], word[2]);
                                Word_reset(word);
                                round = 1;
                            }
                            else if (ja_check(S[i + 1]))    //합쳐진 다음 단어가 자음일 때
                            {
                                word[1] = buffer;
                                round = 3;
                            }
                            else                        //나머지
                            {
                                word[1] = buffer;
                                outputS += MergeJaso(word[0], word[1], word[2]);
                                Word_reset(word);
                                round = 1;
                            }
                        }
                    }
                    else if (ja_check(S[i + 1]))    //다음 단어가 자음일 때
                    {
                        word[1] = buffer;
                        round = 3;
                    }
                    else                        //나머지
                    {
                        word[1] = buffer;
                        outputS += MergeJaso(word[0], word[1], word[2]);
                        Word_reset(word);
                        round = 1;
                    }
                }
                else if (round == 3)
                {
                    if (ja_check(S[i + 1]))     //다음 단어가 자음일 때
                    {
                        if (Jaeum2(buffer, S[i + 1]) == ' ')     //합쳐지지 않는 단어일 때
                        {
                            word[2] = buffer;
                            outputS += MergeJaso(word[0], word[1], word[2]);
                            Word_reset(word);
                            round = 1;
                        }
                        else                                    //합쳐지는 단어일 때
                        {
                            buffer2 = buffer;
                            buffer = Jaeum2(buffer, S[i + 1]);
                            i++;
                            if (ja_check(S[i + 1]))     //합쳐진 다음 단어가 자음일 때
                            {
                                word[2] = buffer;
                                outputS += MergeJaso(word[0], word[1], word[2]);
                                Word_reset(word);
                                round = 1;
                            }
                            else if (mo_check(S[i + 1])) //합쳐진 다음 단어가 모음일 때
                            {
                                word[2] = buffer2;
                                outputS += MergeJaso(word[0], word[1], word[2]);
                                Word_reset(word);
                                word[0] = S[i];
                                round = 2;
                            }
                            else                        //나머지
                            {
                                word[2] = buffer;
                                outputS += MergeJaso(word[0], word[1], word[2]);
                                Word_reset(word);
                                round = 1;
                            }
                        }
                    }
                    else if (mo_check(S[i + 1]))    //다음 단어가 모음일 때
                    {
                        outputS += MergeJaso(word[0], word[1], word[2]);
                        Word_reset(word);
                        word[0] = buffer;
                        round = 2;
                    }
                    else                        //나머지
                    {
                        word[2] = buffer;
                        outputS += MergeJaso(word[0], word[1], word[2]);
                        Word_reset(word);
                        round = 1;
                    }
                }
            }
            string[] output = { outputS, S_en };
            return output;
        }
    }
}

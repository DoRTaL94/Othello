namespace OthelloLogic
{
     public class Square
     {
          private const char m_EmptySquareValue = ' ';
          private char m_Color;

          public Square()
          {
               m_Color = m_EmptySquareValue;
          }

          public char Color
          {
               get
               {
                    return m_Color;
               }

               set
               {
                    m_Color = value;
               }
          }
     }
}

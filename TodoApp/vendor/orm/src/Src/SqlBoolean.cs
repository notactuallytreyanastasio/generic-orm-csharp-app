using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__1923;
        public void FormatTo(T::StringBuilder builder__1925)
        {
            string t___8850;
            if (this.value__1923)
            {
                t___8850 = "TRUE";
            }
            else
            {
                t___8850 = "FALSE";
            }
            builder__1925.Append(t___8850);
        }
        public SqlBoolean(bool value__1928)
        {
            this.value__1923 = value__1928;
        }
        public bool Value
        {
            get
            {
                return this.value__1923;
            }
        }
    }
}

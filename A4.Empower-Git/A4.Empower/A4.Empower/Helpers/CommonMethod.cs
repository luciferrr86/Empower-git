namespace A4.Empower
{
   public static  class CommonMethod
    {
        public static bool checkString(this string value)
        {
            if (!string.IsNullOrEmpty(value) && value != "00000000-0000-0000-0000-000000000000")
            {
                return true;
            }
            return false;
        }
    }
}

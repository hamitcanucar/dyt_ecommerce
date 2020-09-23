namespace dytsenayasar
{
    public static class ErrorMessages
    {
        public const string NOTHING_CHANGED = "Nothing found to be modified!";
        public const string LOGIN_WRONG_CRIDENTIALS = "Wrong email or password!";
        public const string LOGIN_DEACTIVE_USER = "Inactive users cannot receive tokens!";
        public const string DUPLICATED_CRIDENTIAL = "Duplicated personalId or email!";
        public const string USER_NOT_FOUND = "User not found!";
        public const string USER_NOT_FOUND_IN_CORP = "User not found in your corporation!";
        public const string USER_UPDATE_WRONG_OLDPASS = "Wrong old password!";
        public const string CORP_CREATE_CONFLICT = "There are conflicted constraints. Should be: personalId, email";
        public const string FILE_CONTENT_NOT_FOUND = "Content not found! File upload failed.";
        public const string FILE_TOO_BIG = "Too big {0}. Max size: {1} MB";
        public const string FILE_EMPTY = "Empty file! Please select correct file.";
        public const string FILE_TYPE_WRONG = "Wrong file type! Should be: {0}";
    }
}
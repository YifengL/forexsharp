using System;
namespace FXSharp.TradingPlatform.Exts
{
    public static class ErrorHandler
    {
        internal static Exception CreateException(int errorCode)
        {
            return new MQLException(ErrorDescription((MQLError)errorCode));
        }

        private static string ErrorDescription(MQLError error_code)
        {
            string error_string = string.Empty;

            switch (error_code)
            {
                //---- codes returned from trade server
                case MQLError.ERR_NO_ERROR:
                case MQLError.ERR_NO_RESULT:
                    error_string = "no error";
                    break;
                case MQLError.ERR_COMMON_ERROR:
                    error_string = "common error";
                    break;
                case MQLError.ERR_INVALID_TRADE_PARAMETERS:
                    error_string = "invalid trade parameters";
                    break;
                case MQLError.ERR_SERVER_BUSY:
                    error_string = "trade server is busy";
                    break;
                case MQLError.ERR_OLD_VERSION:
                    error_string = "old version of the client terminal";
                    break;
                case MQLError.ERR_NO_CONNECTION:
                    error_string = "no connection with trade server";
                    break;
                case MQLError.ERR_NOT_ENOUGH_RIGHTS:
                    error_string = "not enough rights";
                    break;
                case MQLError.ERR_TOO_FREQUENT_REQUESTS:
                    error_string = "too frequent requests";
                    break;
                case MQLError.ERR_MALFUNCTIONAL_TRADE:
                    error_string = "malfunctional trade operation (never returned error)";
                    break;
                case MQLError.ERR_ACCOUNT_DISABLED:
                    error_string = "account disabled";
                    break;
                case MQLError.ERR_INVALID_ACCOUNT:
                    error_string = "invalid account";
                    break;
                case MQLError.ERR_TRADE_TIMEOUT:
                    error_string = "trade timeout";
                    break;
                case MQLError.ERR_INVALID_PRICE:
                    error_string = "invalid price";
                    break;
                case MQLError.ERR_INVALID_STOPS:
                    error_string = "invalid stops";
                    break;
                case MQLError.ERR_INVALID_TRADE_VOLUME:
                    error_string = "invalid trade volume";
                    break;
                case MQLError.ERR_MARKET_CLOSED:
                    error_string = "market is closed";
                    break;
                case MQLError.ERR_TRADE_DISABLED:
                    error_string = "trade is disabled";
                    break;
                case MQLError.ERR_NOT_ENOUGH_MONEY:
                    error_string = "not enough money";
                    break;
                case MQLError.ERR_PRICE_CHANGED:
                    error_string = "price changed";
                    break;
                case MQLError.ERR_OFF_QUOTES:
                    error_string = "off quotes";
                    break;
                case MQLError.ERR_BROKER_BUSY:
                    error_string = "broker is busy (never returned error)";
                    break;
                case MQLError.ERR_REQUOTE:
                    error_string = "requote";
                    break;
                case MQLError.ERR_ORDER_LOCKED:
                    error_string = "order is locked";
                    break;
                case MQLError.ERR_LONG_POSITIONS_ONLY_ALLOWED:
                    error_string = "long positions only allowed";
                    break;
                case MQLError.ERR_TOO_MANY_REQUESTS:
                    error_string = "too many requests";
                    break;
                case MQLError.ERR_TRADE_MODIFY_DENIED:
                    error_string = "modification denied because order too close to market";
                    break;
                case MQLError.ERR_TRADE_CONTEXT_BUSY:
                    error_string = "trade context is busy";
                    break;
                case MQLError.ERR_TRADE_EXPIRATION_DENIED:
                    error_string = "expirations are denied by broker";
                    break;
                case MQLError.ERR_TRADE_TOO_MANY_ORDERS:
                    error_string = "amount of open and pending orders has reached the limit";
                    break;
                case MQLError.ERR_TRADE_HEDGE_PROHIBITED:
                    error_string = "hedging is prohibited";
                    break;
                case MQLError.ERR_TRADE_PROHIBITED_BY_FIFO:
                    error_string = "prohibited by FIFO rules";
                    break;
                    //---- mql4 errors
                case MQLError.ERR_NO_MQLERROR:
                    error_string = "no error (never generated code)";
                    break;
                case MQLError.ERR_WRONG_FUNCTION_POINTER:
                    error_string = "wrong function pointer";
                    break;
                case MQLError.ERR_ARRAY_INDEX_OUT_OF_RANGE:
                    error_string = "array index is out of range";
                    break;
                case MQLError.ERR_NO_MEMORY_FOR_CALL_STACK:
                    error_string = "no memory for function call stack";
                    break;
                case MQLError.ERR_RECURSIVE_STACK_OVERFLOW:
                    error_string = "recursive stack overflow";
                    break;
                case MQLError.ERR_NOT_ENOUGH_STACK_FOR_PARAM:
                    error_string = "not enough stack for parameter";
                    break;
                case MQLError.ERR_NO_MEMORY_FOR_PARAM_STRING:
                    error_string = "no memory for parameter string";
                    break;
                case MQLError.ERR_NO_MEMORY_FOR_TEMP_STRING:
                    error_string = "no memory for temp string";
                    break;
                case MQLError.ERR_NOT_INITIALIZED_STRING:
                    error_string = "not initialized string";
                    break;
                case MQLError.ERR_NOT_INITIALIZED_ARRAYSTRING:
                    error_string = "not initialized string in array";
                    break;
                case MQLError.ERR_NO_MEMORY_FOR_ARRAYSTRING:
                    error_string = "no memory for array\' string";
                    break;
                case MQLError.ERR_TOO_LONG_STRING:
                    error_string = "too long string";
                    break;
                case MQLError.ERR_REMAINDER_FROM_ZERO_DIVIDE:
                    error_string = "remainder from zero divide";
                    break;
                case MQLError.ERR_ZERO_DIVIDE:
                    error_string = "zero divide";
                    break;
                case MQLError.ERR_UNKNOWN_COMMAND:
                    error_string = "unknown command";
                    break;
                case MQLError.ERR_WRONG_JUMP:
                    error_string = "wrong jump (never generated error)";
                    break;
                case MQLError.ERR_NOT_INITIALIZED_ARRAY:
                    error_string = "not initialized array";
                    break;
                case MQLError.ERR_DLL_CALLS_NOT_ALLOWED:
                    error_string = "dll calls are not allowed";
                    break;
                case MQLError.ERR_CANNOT_LOAD_LIBRARY:
                    error_string = "cannot load library";
                    break;
                case MQLError.ERR_CANNOT_CALL_FUNCTION:
                    error_string = "cannot call function";
                    break;
                case MQLError.ERR_EXTERNAL_CALLS_NOT_ALLOWED:
                    error_string = "expert function calls are not allowed";
                    break;
                case MQLError.ERR_NO_MEMORY_FOR_RETURNED_STR:
                    error_string = "not enough memory for temp string returned from function";
                    break;
                case MQLError.ERR_SYSTEM_BUSY:
                    error_string = "system is busy (never generated error)";
                    break;
                case MQLError.ERR_INVALID_FUNCTION_PARAMSCNT:
                    error_string = "invalid function parameters count";
                    break;
                case MQLError.ERR_INVALID_FUNCTION_PARAMVALUE:
                    error_string = "invalid function parameter value";
                    break;
                case MQLError.ERR_STRING_FUNCTION_INTERNAL:
                    error_string = "string function internal error";
                    break;
                case MQLError.ERR_SOME_ARRAY_ERROR:
                    error_string = "some array error";
                    break;
                case MQLError.ERR_INCORRECT_SERIESARRAY_USING:
                    error_string = "incorrect series array using";
                    break;
                case MQLError.ERR_CUSTOM_INDICATOR_ERROR:
                    error_string = "custom indicator error";
                    break;
                case MQLError.ERR_INCOMPATIBLE_ARRAYS:
                    error_string = "arrays are incompatible";
                    break;
                case MQLError.ERR_GLOBAL_VARIABLES_PROCESSING:
                    error_string = "global variables processing error";
                    break;
                case MQLError.ERR_GLOBAL_VARIABLE_NOT_FOUND:
                    error_string = "global variable not found";
                    break;
                case MQLError.ERR_FUNC_NOT_ALLOWED_IN_TESTING:
                    error_string = "function is not allowed in testing mode";
                    break;
                case MQLError.ERR_FUNCTION_NOT_CONFIRMED:
                    error_string = "function is not confirmed";
                    break;
                case MQLError.ERR_SEND_MAIL_ERROR:
                    error_string = "send mail error";
                    break;
                case MQLError.ERR_STRING_PARAMETER_EXPECTED:
                    error_string = "string parameter expected";
                    break;
                case MQLError.ERR_INTEGER_PARAMETER_EXPECTED:
                    error_string = "integer parameter expected";
                    break;
                case MQLError.ERR_DOUBLE_PARAMETER_EXPECTED:
                    error_string = "double parameter expected";
                    break;
                case MQLError.ERR_ARRAY_AS_PARAMETER_EXPECTED:
                    error_string = "array as parameter expected";
                    break;
                case MQLError.ERR_HISTORY_WILL_UPDATED:
                    error_string = "requested history data in update state";
                    break;
                case MQLError.ERR_TRADE_ERROR:
                    error_string = "Trade error";
                    break;
                case MQLError.ERR_END_OF_FILE:
                    error_string = "end of file";
                    break;
                case MQLError.ERR_SOME_FILE_ERROR:
                    error_string = "some file error";
                    break;
                case MQLError.ERR_WRONG_FILE_NAME:
                    error_string = "wrong file name";
                    break;
                case MQLError.ERR_TOO_MANY_OPENED_FILES:
                    error_string = "too many opened files";
                    break;
                case MQLError.ERR_CANNOT_OPEN_FILE:
                    error_string = "cannot open file";
                    break;
                case MQLError.ERR_INCOMPATIBLE_FILEACCESS:
                    error_string = "incompatible access to a file";
                    break;
                case MQLError.ERR_NO_ORDER_SELECTED:
                    error_string = "no order selected";
                    break;
                case MQLError.ERR_UNKNOWN_SYMBOL:
                    error_string = "unknown symbol";
                    break;
                case MQLError.ERR_INVALID_PRICE_PARAM:
                    error_string = "invalid price parameter for trade function";
                    break;
                case MQLError.ERR_INVALID_TICKET:
                    error_string = "invalid ticket";
                    break;
                case MQLError.ERR_TRADE_NOT_ALLOWED:
                    error_string = "trade is not allowed in the expert properties";
                    break;
                case MQLError.ERR_LONGS_NOT_ALLOWED:
                    error_string = "longs are not allowed in the expert properties";
                    break;
                case MQLError.ERR_SHORTS_NOT_ALLOWED:
                    error_string = "shorts are not allowed in the expert properties";
                    break;
                case MQLError.ERR_OBJECT_ALREADY_EXISTS:
                    error_string = "object is already exist";
                    break;
                case MQLError.ERR_UNKNOWN_OBJECT_PROPERTY:
                    error_string = "unknown object property";
                    break;
                case MQLError.ERR_OBJECT_DOES_NOT_EXIST:
                    error_string = "object is not exist";
                    break;
                case MQLError.ERR_UNKNOWN_OBJECT_TYPE:
                    error_string = "unknown object type";
                    break;
                case MQLError.ERR_NO_OBJECT_NAME:
                    error_string = "no object name";
                    break;
                case MQLError.ERR_OBJECT_COORDINATES_ERROR:
                    error_string = "object coordinates error";
                    break;
                case MQLError.ERR_NO_SPECIFIED_SUBWINDOW:
                    error_string = "no specified subwindow";
                    break;
                case MQLError.ERR_SOME_OBJECT_ERROR:
                    error_string = "some object error";
                    break;
                default:
                    error_string = "unknown error";
                    break;
            }
//----
            return (error_string);
        }
    }
}
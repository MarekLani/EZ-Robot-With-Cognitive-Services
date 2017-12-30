using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication
{
    class WavRecording
    {

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int Record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        public string StartRecording()
        {
            var result = (MCIErrors)Record("open new Type waveaudio Alias recsound", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                return "Error code: " + result.ToString();
              
            }
            //Setting specific settings for output wav format, so it meets requirements of Speaker Recognition service
            result = (MCIErrors)Record("set recsound time format ms alignment 2 bitspersample 16 samplespersec 16000 channels 1 bytespersec 88200", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                return "Error code: " + result.ToString();
               
            }

            result = (MCIErrors)Record("record recsound", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                return "Error code: " + result.ToString();
               
            }
            return "1";
        }

        public string StopRecording()
        {
            var result = (MCIErrors)Record("save recsound result.wav", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                return "Error code: " + result.ToString();
               
            }
            result = (MCIErrors)Record("close recsound ", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                return "Error code: " + result.ToString();
            }

            return "1";
        }
    }

    ///Stack overflow: Error outputting for interop https://stackoverflow.com/questions/28613108/how-to-record-wav-in-c-sharp-wpf
    public enum MCIErrors
    {
        NO_ERROR = 0,
        MCIERR_BASE = 256,
        MCIERR_INVALID_DEVICE_ID = 257,
        MCIERR_UNRECOGNIZED_KEYWORD = 259,
        MCIERR_UNRECOGNIZED_COMMAND = 261,
        MCIERR_HARDWARE = 262,
        MCIERR_INVALID_DEVICE_NAME = 263,
        MCIERR_OUT_OF_MEMORY = 264,
        MCIERR_DEVICE_OPEN = 265,
        MCIERR_CANNOT_LOAD_DRIVER = 266,
        MCIERR_MISSING_COMMAND_STRING = 267,
        MCIERR_PARAM_OVERFLOW = 268,
        MCIERR_MISSING_STRING_ARGUMENT = 269,
        MCIERR_BAD_INTEGER = 270,
        MCIERR_PARSER_INTERNAL = 271,
        MCIERR_DRIVER_INTERNAL = 272,
        MCIERR_MISSING_PARAMETER = 273,
        MCIERR_UNSUPPORTED_FUNCTION = 274,
        MCIERR_FILE_NOT_FOUND = 275,
        MCIERR_DEVICE_NOT_READY = 276,
        MCIERR_INTERNAL = 277,
        MCIERR_DRIVER = 278,
        MCIERR_CANNOT_USE_ALL = 279,
        MCIERR_MULTIPLE = 280,
        MCIERR_EXTENSION_NOT_FOUND = 281,
        MCIERR_OUTOFRANGE = 282,
        MCIERR_FLAGS_NOT_COMPATIBLE = 283,
        MCIERR_FILE_NOT_SAVED = 286,
        MCIERR_DEVICE_TYPE_REQUIRED = 287,
        MCIERR_DEVICE_LOCKED = 288,
        MCIERR_DUPLICATE_ALIAS = 289,
        MCIERR_BAD_CONSTANT = 290,
        MCIERR_MUST_USE_SHAREABLE = 291,
        MCIERR_MISSING_DEVICE_NAME = 292,
        MCIERR_BAD_TIME_FORMAT = 293,
        MCIERR_NO_CLOSING_QUOTE = 294,
        MCIERR_DUPLICATE_FLAGS = 295,
        MCIERR_INVALID_FILE = 296,
        MCIERR_NULL_PARAMETER_BLOCK = 297,
        MCIERR_UNNAMED_RESOURCE = 298,
        MCIERR_NEW_REQUIRES_ALIAS = 299,
        MCIERR_NOTIFY_ON_AUTO_OPEN = 300,
        MCIERR_NO_ELEMENT_ALLOWED = 301,
        MCIERR_NONAPPLICABLE_FUNCTION = 302,
        MCIERR_ILLEGAL_FOR_AUTO_OPEN = 303,
        MCIERR_FILENAME_REQUIRED = 304,
        MCIERR_EXTRA_CHARACTERS = 305,
        MCIERR_DEVICE_NOT_INSTALLED = 306,
        MCIERR_GET_CD = 307,
        MCIERR_SET_CD = 308,
        MCIERR_SET_DRIVE = 309,
        MCIERR_DEVICE_LENGTH = 310,
        MCIERR_DEVICE_ORD_LENGTH = 311,
        MCIERR_NO_INTEGER = 312,
        MCIERR_WAVE_OUTPUTSINUSE = 320,
        MCIERR_WAVE_SETOUTPUTINUSE = 321,
        MCIERR_WAVE_INPUTSINUSE = 322,
        MCIERR_WAVE_SETINPUTINUSE = 323,
        MCIERR_WAVE_OUTPUTUNSPECIFIED = 324,
        MCIERR_WAVE_INPUTUNSPECIFIED = 325,
        MCIERR_WAVE_OUTPUTSUNSUITABLE = 326,
        MCIERR_WAVE_SETOUTPUTUNSUITABLE = 327,
        MCIERR_WAVE_INPUTSUNSUITABLE = 328,
        MCIERR_WAVE_SETINPUTUNSUITABLE = 329,
        MCIERR_SEQ_DIV_INCOMPATIBLE = 336,
        MCIERR_SEQ_PORT_INUSE = 337,
        MCIERR_SEQ_PORT_NONEXISTENT = 338,
        MCIERR_SEQ_PORT_MAPNODEVICE = 339,
        MCIERR_SEQ_PORT_MISCERROR = 340,
        MCIERR_SEQ_TIMER = 341,
        MCIERR_SEQ_PORTUNSPECIFIED = 342,
        MCIERR_SEQ_NOMIDIPRESENT = 343,
        MCIERR_NO_WINDOW = 346,
        MCIERR_CREATEWINDOW = 347,
        MCIERR_FILE_READ = 348,
        MCIERR_FILE_WRITE = 349,
        MCIERR_CUSTOM_DRIVER_BASE = 512
    };
}

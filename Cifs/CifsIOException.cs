namespace Cifs
{
    using System;
    using System.IO;

    using Cifs.Util;

    /// <summary>
    ///    Base class for all exceptions
    ///    
    ///    Enhance later
    /// </summary>
    public class CifsIoException: IOException
    {
        /// <summary>
        /// The request was successful
        /// </summary>
        public const byte SUCCESS   = (byte)0;

        /// <summary>
        /// Error is from the core DOS operating system set
        /// </summary>
        public const byte ERROR_DOS = (byte)0x01;

        /// <summary>
        /// Error is generated by the server network file manager
        /// </summary>
        public const byte ERROR_SRV = (byte)0x02;

        /// <summary>
        /// Error is a hardware error
        /// </summary>
        public const byte ERROR_HW  = (byte)0x03;

        /// <summary>
        /// Command was not in the "SMB" format
        /// </summary>
        public const byte ERROR_CMD = (byte)0xFF;

        /**
         *                      ERROR_DOS
         * The following error codes may be generated with the ERROR_DOS error class.
         * When an SMB dialect greater than equal to LANMAN 1.0 has been nego-tiated,
         * all of the error codes below may be generated plus any of the error codes
         * defined for OS/2
         */

        /** Invalid function.  The server did not recognize or could not perform a
         * system call generated by the server,
         * e.g., set the DIRECTORY attribute on a data file, invalid seek mode.*/
        public const short DOS_BAD_FUNCTION          = 1;
        /** File not found.  The last component of a file's pathname could not be found. */
        public const short DOS_BAD_FILE    	        = 2;
        /** Directory invalid.  A directory component in a pathname could not be found. */
        public const short DOS_BAD_PATH 	            = 3	;
        /**  Too many open files.  The server has no file handles available.*/
        public const short DOS_NO_FILE_HANDLE	    = 4	;
        /**  Access denied, the client's context does not permit the requested function.
         * This includes the following conditions:
         * - invalid rename command
         * - write to fid open for read only
         * - read on fid open for write only
         * - attempt to delete a non-empty directory */
        public const short DOS_NO_ACCESS 	        = 5	;
        /** Invalid file handle.  The file handle specified was not recognized by the server.*/
        public const short DOS_BAD_FILE_HANDLE 	    = 6	;
        /** 	Memory control blocks destroyed.*/
        public const short DOS_BAD_MCB	            = 7;
        /** Insufficient server memory to perform the requested function.*/
        public const short DOS_NO_MEMORY 	        = 8	;
        /** Invalid memory block address.*/
        public const short DOS_BAD_MEMORY 	        = 9	;
        /** Invalid environment.*/
        public const short DOS_BAD_ENVIRONME	        = 10;
        /** Invalid format.*/
        public const short DOS_BAD_FORMAT 	        = 11;
        /** Invalid open mode.*/
        public const short DOS_BAD_ACCESS_MODE 	    = 12;
        /** Invalid data (generated only by IOCTL calls within the server).*/
        public const short DOS_BAD_DATA      	    = 13;
        /** Invalid drive specified.*/
        public const short DOS_BAD_DRIVE     	    = 15;
        /** A Delete Directory request attempted to remove the server's current directory.*/
        public const short DOS_REM_CD 	            = 16;
        /**  Not same device (e.g., a cross volume rename was attempted)*/
        public const short DOS_DIFF_DEVICE 	        = 17;
        /**  A File Search command can find no more files matching the specified criteria.*/
        public const short DOS_NO_FILES 	            = 18;
        /**  The sharing mode specified for an Open conflicts with existing FIDs on the file.*/
        public const short DOS_BAD_SHARE 	        = 32;
        /**  A Lock request conflicted with an existing lock or specified an invalid mode,
         * or an Unlock requested attempted to remove a lock held by another process.*/
        public const short DOS_LOCK_CONFLICT	        = 33;
        /** The file named in a Create Directory, Make New File or Link request already exists.
         * The error may also be generated in the Create and Rename transaction.*/
        public const short DOS_FILE_EXISTS 	        = 80;
        /** Pipe invalid.*/
        public const short DOS_BAD_PIPE 	            = 230;
        /** All instances of the requested pipe are busy.*/
        public const short DOS_PIPE_BUSY 	        = 231;
        /** Pipe close in progress.*/
        public const short DOS_PIPE_CLOSING 	        = 232;
        /** No process on other end of pipe.*/
        public const short DOS_NOT_CONNECTED 	    = 233;
        /** 	There is more data to be returned.*/
        public const short DOS_MORE_DATA 	        = 234;

        /**
         *                      ERROR_SRV error class
         * The following error codes may be generated with the ERROR_SRV error class.
         */

        /** Non-specific error code.*/
        // It is returned under the following conditions:
        // - resource other than disk space exhausted (e.g.  TIDs)
        // - first SMB command was not negotiate
        // - multiple negotiates attempted
        // - internal server error
        public const short SRV_ERROR	                = 1;

        /** Bad password - name/password pair in a Tree Connect or Session Setup are invalid..*/
        public const short SRV_BAD_PASSWORD 	        = 2;
        /** The client does not have the necessary access rights within the specified context
         * for the requested function..*/
        public const short SRV_NO_ACCESS 	        = 4;
        /** The Tid specified in a command was invalid..*/
        public const short SRV_BAD_TID 	            = 5;
        /** Invalid network name in tree connect..*/
        public const short SRV_BAD_NETWORK_NAME      = 6;
        /**Invalid device - printer request made to non-printer connection or
         * non-printer request made to printer connection..*/
        public const short SRV_BAD_DEVICE	        = 7;
        /** Print queue full (files) -- returned by open print file..*/
        public const short SRV_PRINT_QUEUE_FULL      = 49;
        /** Print queue full -- no space..*/
        public const short SRV_PRINT_QUEUE_NO_SPACE  = 50;
        /** EOF on print queue dump..*/
        public const short SRV_EOF_PRINT_QUEUE       = 51;
        /** Invalid print file FID..*/
        public const short SRV_BAD_PRINT_FILE_FID    = 52;
        /** The server did not recognize the command received..*/
        public const short SRV_BAD_COMMAND	        = 64;
        /** The server encountered an internal error, e.g., system file unavailable..*/
        public const short SRV_INTERNAL_ERROR	    = 65;
        /** The Fid and pathname parameters contained an invalid combination of values.*/
        public const short SRV_BAD_FILE_SPECS	    = 67;
        /** The access permissions specified for a file or directory are not a
         * valid combination.  The server cannot set the requested attribute.*/
        public const short SRV_BAD_PERMITS	        = 69;
        /** The attribute mode in the Set File Attribute request is invalid.*/
        public const short SRV_BAD_SET_ATTR_MODE 	= 71;
        /** Server is paused.  (reserved for messaging)*/
        public const short SRV_PAUSED	            = 81;
        /** Not receiving messages.  (reserved for messaging).*/
        public const short SRV_NOT_RCV_MESSAGES	    = 82;
        /** No room to buffer message.  (reserved for messaging).*/
        public const short SRV_NO_ROOM	            = 83;
        /** Too many remote user names.  (reserved for messaging).*/
        public const short SRV_TOO_MANY_REMOTE_USERS = 87;
        /** Operation timed out.*/
        public const short SRV_TIMEOUT	            = 88	;
        /** No resources currently available for request.*/
        public const short SRV_NO_RESOURCES	        = 89;
        /** 	Too many Uids active on this session.*/
        public const short SRV_TOO_MANY_UIDS	        = 90;
        /** The Uid is not known as a valid user identifier on this session.*/
        public const short SRV_BAD_UID	            = 91;
        /** Temporarily unable to support Raw, use MPX mode.*/
        public const short SRV_USE_MPX	            = 250;
        /** 	Temporarily unable to support Raw, use standard read/write.*/
        public const short SRV_USE_STD	            = 251;
        /** 	Continue in MPX mode.*/
        public const short SRV_CONT_MPX_MODE	        = 252;
        /** Function not supported.*/
        public const ushort SRV_FCT_NOT_SUPPORTED     = (ushort)65535	;

        /*
         * The following error codes may be generated with the ERROR_HW error class.
         */
        /** Attempt to write on write-protected media*/
        public const short HW_NO_WRITE 	            = 19;
        /** Unknown unit.*/
        public const short HW_BAD_UNIT	            = 20;
        /** Drive not ready.*/
        public const short HW_NOT_READY      	    = 21;
        /** Unknown command.*/
        public const short HW_BAD_COMMAND	        = 22;
        /** Data error (CRC).*/
        public const short HW_DATA_ERROR 	        = 23;
        /** Bad request structure length.*/
        public const short HW_BAD_REQUEST	        = 24;
        /** Seek error.*/
        public const short HW_SEEK_ERROR	            = 25;
        /** Unknown media type.*/
        public const short HW_BAD_MEDIA      	    = 26;
        /** Sector not found.*/
        public const short HW_BAD_SECTOR     	    = 27;
        /** Printer out of paper.*/
        public const short HW_NO_PAPER       	    = 28;
        /** Write fault.*/
        public const short HW_WRITE_FAULT	        = 29;
        /** 	Read fault.*/
        public const short HW_READ_FAULT 	        = 30;
        /** General failure.*/
        public const short HW_GENERAL_FAILURE	    = 31;
        /** A open conflicts with an existing open.*/
        public const short HW_BAD_SHARE	            = 32;
        /** A Lock request conflicted with an existing lock or specified an invalid mode,
         * or an Unlock requested attempted to remove a lock held by another process.*/
        public const short HW_LOCK_CONFLICT	        = 33;
        /** The wrong disk was found in a drive.*/
        public const short HW_WRONG_DISK     	    = 34;
        /** No FCBs are available to process request.*/
        public const short HW_FC_NOT_AVAILABLE	    = 35;
        /** 	A sharing buffer has been exceeded.*/
        public const short HW_SHARE_BUFFER_EXCEEDED	= 36;

        private String    fMessage;
        private Exception fDetail = null;
        private int       fSMBErrorClass = 0;
        private int       fSMBErrorCode  = 0;

        private bool   fConnectionLost = false;

        // We don't want people throwing blank exceptions
        private CifsIoException()
        {			
        }
        
        internal static CifsIoException getNBException(int code)
        {
            CifsIoException e = new CifsIoException();
            
            try
            {
                e.fMessage = CifsRuntimeException.GetMessage("NB" + code);
            }
            catch(Exception)
            {
                e.fMessage = CifsRuntimeException.GetMessage("NB999", code);
            }
            return e;
        }

        internal static CifsIoException getLMException(int code)
        {
            CifsIoException e = new CifsIoException();
            try
            {
                e.fMessage = CifsRuntimeException.GetMessage("LM" + code);
            }
            catch(Exception)
            {
                e.fMessage = CifsRuntimeException.GetMessage("LMERROR", code);
                if(Debug.DebugOn)
                    Debug.WriteLine(Debug.Warning,"LM Error: " + code + " not defined.");
            }
            return e;
            
        }

        public CifsIoException(string key)
        {
            fMessage = CifsRuntimeException.GetMessage(key);
        }
        
        internal CifsIoException(int errorclass, int errorcode)
        {
            fSMBErrorClass = errorclass;
            fSMBErrorCode  = errorcode;
            string key;

            if (errorclass == ERROR_CMD)
                key = "255:0";
            else
                key = (errorclass & 0xff) + ":" + (errorcode & 0xffff);

            try
            {
                fMessage = CifsRuntimeException.GetMessage(key);
            }
            catch(Exception) // The key was not found
            {
                fMessage = CifsRuntimeException.GetMessage("U:U");
            }

            fMessage = fMessage + "( SMB=" + key + ")";
        }

        internal CifsIoException(string key, object i1)
        {
            fMessage = CifsRuntimeException.GetMessage(key, i1);
        }

        internal CifsIoException(string key, object i1, object i2)
        {
            fMessage = CifsRuntimeException.GetMessage(key, i1, i2);
        }

        internal CifsIoException setDetail(Exception detail)
        {
            fDetail = detail;
            return this;
        }

        public bool isSMBError()
        {
            return (fSMBErrorCode != 0 && fSMBErrorClass != 0);
        }

        public int getSMBErrorClass()
        {
            return fSMBErrorClass;
        }

        public int getSMBErrorCode()
        {
            return fSMBErrorCode;
        }

        public bool isConnectionLost()
        {
            return fConnectionLost;
        }

        internal CifsIoException setConnectionLost()
        {
            fConnectionLost = true;
            return this;
        }

        public string getMessage()
        {
            if (fDetail != null)
                return fMessage + " [" + fDetail.Message + "]";

            return fMessage;
        }
    } // class CifsIOException
} // namespace Cifs

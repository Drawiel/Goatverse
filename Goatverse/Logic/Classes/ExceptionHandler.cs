using Goatverse.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Goatverse.Logic.Classes {
    public static class ExceptionHandler {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void HandleServiceException(Exception ex) {
            if (ex is EndpointNotFoundException) {
                MessageBox.Show(Lang.messageServerLostConnection);
                log.Warn($"EndpointNotFound: {ex.Message}", ex);
            } else if (ex is TimeoutException) {
                MessageBox.Show(Lang.messageConnectionTookTooLong);
                log.Warn($"TimeoutException: {ex.Message}", ex);
            } else if (ex is CommunicationException) {
                MessageBox.Show(Lang.messageDatabaseError);
                log.Warn($"CommunicationException:  {ex.Message}", ex);
            } else {
                MessageBox.Show(Lang.messageUnexpectedError);
                log.Error($"UnexpectedException: {ex.Message}", ex);
            }
        }
    }
}

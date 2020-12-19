using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PiyatMandli
{
    public static class AppManager
    {
        /// <summary>
        /// All the config values are available here
        /// </summary>
        public static Dictionary<string, string> Configs { get; set; }
        /// <summary>
        /// Application Start up form
        /// </summary>
        //public static MasterForm MasterForm { get; set; }
        /// <summary>
        /// Flag Names constatns
        /// </summary>
        public static class ApplicationFlag
        {
            public static string ApplicationName = "Application Name";
        }
        /// <summary>
        /// Get the value from Appmanager.Configs
        /// </summary>
        /// <param name="flagName"></param>
        /// <returns></returns>
        public static string GetFlag(string flagName)
        {
            if (Configs != null)
            {
                if (Configs.ContainsKey(flagName))
                {
                    return Configs[flagName];
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// only numbers will be converted
        /// </summary>
        /// <param name="englishString"></param>
        /// <returns></returns>
        public static string ToGujarati(this string englishString)
        {
            if (!string.IsNullOrEmpty(englishString))
            {
                string value = string.Empty;
                foreach (var c in englishString)
                {
                    switch (c)
                    {
                        case '1':
                            value = value + "૧";
                            break;
                        case '2':
                            value = value + "૨";
                            break;
                        case '3':
                            value = value + "૩";
                            break;
                        case '4':
                            value = value + "૪";
                            break;
                        case '5':
                            value = value + "૫";
                            break;
                        case '6':
                            value = value + "૬";
                            break;
                        case '7':
                            value = value + "૭";
                            break;
                        case '8':
                            value = value + "૮";
                            break;
                        case '9':
                            value = value + "૯";
                            break;
                        case '0':
                            value = value + "૦";
                            break;
                        default:
                            value = value + c.ToString();
                            break;
                    }
                }
                return value;
            }
            return "";
        }

        /// <summary>
        /// only numbers will be converted
        /// </summary>
        /// <param name="gujaratiString"></param>
        /// <returns></returns>
        public static string ToEnglish(this string gujaratiString)
        {
            if (!string.IsNullOrEmpty(gujaratiString))
            {
                string value = string.Empty;
                foreach (var c in gujaratiString)
                {
                    switch (c)
                    {
                        case '૧':
                            value = value + "1";
                            break;
                        case '૨':
                            value = value + "2";
                            break;
                        case '૩':
                            value = value + "3";
                            break;
                        case '૪':
                            value = value + "4";
                            break;
                        case '૫':
                            value = value + "5";
                            break;
                        case '૬':
                            value = value + "6";
                            break;
                        case '૭':
                            value = value + "7";
                            break;
                        case '૮':
                            value = value + "8";
                            break;
                        case '૯':
                            value = value + "9";
                            break;
                        case '૦':
                            value = value + "0";
                            break;
                        default:
                            value = value + c.ToString();
                            break;
                    }
                }
                return value;
            }
            return "";
        }

        /// <summary>
        /// Get month name in gujarati 
        /// </summary>
        /// <param name="month">1 to 12</param>
        /// <returns></returns>
        public static string GetMonthInGujarati(int month)
        {
            if (month > 0 && month <= 12)
            {
                string m = "";
                switch (month)
                {
                    case 1:
                        m = "જાન્યુઆરી";
                        break;
                    case 2:
                        m = "ફેબ્રુઆરી";
                        break;
                    case 3:
                        m = "માર્ચ";
                        break;
                    case 4:
                        m = "એપ્રિલ";
                        break;
                    case 5:
                        m = "મે";
                        break;
                    case 6:
                        m = "જુન";
                        break;
                    case 7:
                        m = "જુલાઇ";
                        break;
                    case 8:
                        m = "ઓગસ્ટ";
                        break;
                    case 9:
                        m = "સપ્ટેમ્બર";
                        break;
                    case 10:
                        m = "ઓક્ટોબર";
                        break;
                    case 11:
                        m = "નવેમ્બર";
                        break;
                    case 12:
                        m = "ડીસેમ્બર";
                        break;
                }
                return m;
            }
            else
            {
                throw new Exception("Invalid month " + month.ToString());
            }
        }

        /// <summary>
        /// Logging exceptions
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="arguments"></param>
        /// <param name="message"></param>
        public static string LogException(Exception ex, object arguments = null, string message = null)
        {
            try
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                }
                return errorMessage;
            }
            catch
            {
                return "";
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static DependencyObject FindChild(this DependencyObject reference, string childName, Type childType)
        {
            DependencyObject foundChild = null;
            if (reference != null)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(reference);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(reference, i);
                    // If the child is not of the request child type child
                    if (child.GetType() != childType)
                    {
                        // recursively drill down the tree
                        foundChild = FindChild(child, childName, childType);
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = child;
                            break;
                        }
                    }
                    else
                    {
                        // child element found.
                        foundChild = child;
                        break;
                    }
                }
            }
            return foundChild;
        }
    }
}

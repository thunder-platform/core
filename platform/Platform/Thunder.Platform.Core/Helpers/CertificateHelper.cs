using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Thunder.Platform.Core.Exceptions;

namespace Thunder.Platform.Core.Helpers
{
    public static class CertificateHelper
    {
        public static X509Certificate2 FindCertificate(
            string subjectName,
            string serialNumber,
            StoreName name = StoreName.My,
            StoreLocation location = StoreLocation.LocalMachine)
        {
            var store = new X509Store(name, location);
            store.Open(OpenFlags.ReadOnly);
            try
            {
                bool ValidSubjectName(X509Certificate2 c)
                {
                    return c.SubjectName.Name != null && c.SubjectName.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase);
                }

                bool ValidSerialNumber(X509Certificate2 c)
                {
                    return c.SerialNumber != null && c.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase);
                }

                X509Certificate2 cert = store.Certificates.OfType<X509Certificate2>()
                    .FirstOrDefault(c => ValidSubjectName(c) && ValidSerialNumber(c));

                if (cert == null)
                {
                    throw new GeneralException("Certificate is not found! Please install certificate before using it.");
                }

                return new X509Certificate2(cert);
            }
            finally
            {
                store.Certificates.OfType<X509Certificate2>().ToList().ForEach(c => c.Reset());
                store.Close();
            }
        }
    }
}

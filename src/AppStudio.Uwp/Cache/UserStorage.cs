using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

#if UWP
using Windows.Storage;
using Windows.Storage.Search;
namespace AppStudio.Uwp.Cache
#else
using PCLStorage;
namespace AppStudio.Xamarin.Cache
#endif
{
    public static class UserStorage
    {
        public static async Task<string> ReadTextFromFileAsync(string fileName)
        {
            try
            {
#if UWP
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.TryGetFileAsync(fileName);
                if (file != null)
                {
                    return await FileIO.ReadTextAsync(file);
                }
#else
                IFolder folder = FileSystem.Current.LocalStorage;
                IFile file = await folder.GetFileAsync(fileName).ConfigureAwait(false); ;
                return await file.ReadAllTextAsync();
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return String.Empty;
        }

        public static async Task<List<string>> GetMatchingFilesByPrefixAsync(string prefix, List<string> excludeFiles)
        {
            try
            {
                List<string> result = new List<string>();
#if UWP
                var folder = ApplicationData.Current.LocalFolder;
                QueryOptions queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new List<string>() { "*" });
                queryOptions.UserSearchFilter = $"{prefix}*.*";
                queryOptions.FolderDepth = FolderDepth.Shallow;
                queryOptions.IndexerOption = IndexerOption.UseIndexerWhenAvailable;
                StorageFileQueryResult queryResult = folder.CreateFileQueryWithOptions(queryOptions);

                IReadOnlyList<StorageFile> matchingFiles = await queryResult.GetFilesAsync();

                if (matchingFiles.Count > 0) {
                    result.AddRange(
                        matchingFiles.Where(f => !excludeFiles.Contains(f.Name)).Select(f => f.Name)
                    );
                }
                if (matchingFiles.Count > 0)
                {
                    result.AddRange(
                        matchingFiles.Where(f => !excludeFiles.Contains(f.Name)).Select(f => f.Name)
                    );
                }
#else
                IFolder folder = FileSystem.Current.LocalStorage;
                IList<IFile> matchingFiles = await folder.GetFilesAsync();
                if (matchingFiles.Count > 0)
                {
                    result.AddRange(
                        matchingFiles.Where(f => !excludeFiles.Contains(f.Name) && f.Name.StartsWith(prefix)).Select(f => f.Name)
                    );
                }
#endif
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task WriteTextAsync(string fileName, string content)
        {
            try
            {
#if UWP
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, content);
#else
                IFolder folder = FileSystem.Current.LocalStorage;
                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await file.WriteAllTextAsync(content);
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static async Task DeleteFileIfExistsAsync(string fileName)
        {
            try
            {
#if UWP
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.TryGetFileAsync(fileName);
                if (file != null)
                {
                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
#else
                IFolder folder = FileSystem.Current.LocalStorage;
                IFile file = await folder.GetFileAsync(fileName);
                if (file != null)
                {
                    await file.DeleteAsync();
                }
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static async Task AppendLineToFileAsync(string fileName, string line)
        {
            try
            {
#if UWP
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                await FileIO.AppendLinesAsync(file, new List<string>() { line });
#else
                IFolder folder = FileSystem.Current.LocalStorage;
                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                await file.AppendAllLinesAsync(new List<string>() { line });
#endif
            }
            catch { /* Avoid any exception at this point. */ }
        }
#if !UWP
        // Because of the PCLStorage not updated (these 2 functions are in the pull requests)

        /// <summary>
        /// Appends lines to a file, and then closes the file.
        /// </summary>
        /// <param name="file">The file to write to</param>
        /// <param name="contents">The content to write to the file</param>
        /// <returns>A task which completes when the write operation finishes</returns>
        private static async Task AppendAllLinesAsync(this IFile file, IEnumerable<string> contents)
        {
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite).ConfigureAwait(false))
            {
                stream.Seek(stream.Length, SeekOrigin.Begin);
                using (var sw = new StreamWriter(stream))
                {
                    foreach (var content in contents)
                    {
                        await sw.WriteLineAsync(content).ConfigureAwait(false);
                    }
                }
            }
        }

        /// <summary>
        /// Appends lines to a file, and then closes the file.
        /// </summary>
        /// <param name="file">The file to write to</param>
        /// <param name="contents">The content to write to the file</param>
        /// <returns>A task which completes when the write operation finishes</returns>
        private static async Task AppendAllLinesAsync(this IFile file, params string[] contents)
        {
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite).ConfigureAwait(false))
            {
                stream.Seek(stream.Length, SeekOrigin.Begin);
                using (var sw = new StreamWriter(stream))
                {
                    foreach (var content in contents)
                    {
                        await sw.WriteLineAsync(content).ConfigureAwait(false);
                    }
                }
            }
        }
#endif
    }
}

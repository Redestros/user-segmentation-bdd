import * as fs from "fs";
import * as path from "path";

export class FileHandler {
  /**
   *
   * @param {String} path
   */
  constructor(path) {
    this.path = path;
  }

  /**
   *
   * @param {String} extension without the "."
   * @returns {Array} files
   */
  getFilesByExtension(extension) {
    const allFiles = this.getAllFiles(this.path);
    return this.filterFilesByExtension(allFiles, extension);
  }

  /**
   * @private
   * @param {Array} files
   * @param {String} extension
   */
  filterFilesByExtension(files, extension) {
    return files.filter(
      (file) => path.extname(file).toLowerCase() === `.${extension}`
    );
  }

  /**
   * @private
   * @param {String} dir path of the directory
   */
  getAllFiles(dir = this.path) {
    let files = fs.readdirSync(dir);
    files = files.map((file) => {
      const filePath = path.join(dir, file);
      const stats = fs.statSync(filePath);
      if (stats.isDirectory()) return this.getAllFiles(filePath);
      else if (stats.isFile()) return filePath;
    });
    return files.reduce(
      (all, folderContents) => all.concat(folderContents),
      []
    );
  }

  getFileContent(file) {
    return fs.readFileSync(file, "utf-8").toString();
  }
}
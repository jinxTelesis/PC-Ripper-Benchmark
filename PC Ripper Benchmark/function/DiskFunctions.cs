﻿using System;
using PC_Ripper_Benchmark.util;

using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.function {

    /// <summary>
    /// The <see cref="DiskFunctions"/> class.
    /// Represents all the functions for 
    /// testing the disk/ssd component. Includes single
    /// and multithreaded testing using various
    /// custom algorithms for read/writing..
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class DiskFunctions {

        /// <summary>
        /// Default constructor.
        /// </summary>

        public DiskFunctions() {

        }

        /// <summary>
        /// Runs the benchmarking test on the disk
        /// with a particular threading type.
        /// </summary>
        /// <param name="threadType">The type of threading 
        /// for the test.</param>
        /// <returns>A new <see cref="DiskFunctions"/> instance
        /// containing the result.</returns>

        public DiskResults RunDiskBenchmark(ThreadType threadType) {
            var results = new DiskResults();

            Action run_funcs = new Action(() => {

            });

            return results;
        }

        /// <summary>
        /// Creates random directories and sub directories 
        /// on the filesystem, and places files in some,
        /// and searches the random filestructure for the 
        /// given files.
        /// <para>Intensive unit test.</para>
        /// 
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunFolderMatrix() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a bunch of random characters
        /// to a file and 
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunBulkFile() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunReadWriteParse() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Runs <see cref="RunBulkFile"/> in every
        /// directory generated by <see cref="RunFolderMatrix"/>
        /// <para>Very intensive task.</para>
        /// </summary>
        /// <returns></returns>

        private TimeSpan DiskRipper() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Just creates the folder structure and doesn't perform a search
        /// </summary>

        private void FolderMatrixSnapshot() {

        }
    }
}

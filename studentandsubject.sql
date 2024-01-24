-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 24, 2024 at 08:49 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `studentandsubject`
--

-- --------------------------------------------------------

--
-- Table structure for table `logindetails`
--

CREATE TABLE `logindetails` (
  `Id` int(11) NOT NULL,
  `user_name` longtext NOT NULL,
  `password` longtext NOT NULL,
  `token` longtext DEFAULT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `UpdatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `relationship`
--

CREATE TABLE `relationship` (
  `StudentsId` int(11) NOT NULL,
  `SubjectsId` bigint(20) NOT NULL,
  `StudentId` int(11) NOT NULL,
  `SubjectId` int(11) NOT NULL,
  `SubjectId1` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `Id` int(11) NOT NULL,
  `first_name` longtext NOT NULL,
  `last_name` longtext NOT NULL,
  `address` longtext NOT NULL,
  `email` longtext NOT NULL,
  `user_name` longtext NOT NULL,
  `password` longtext NOT NULL,
  `contact_number` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`Id`, `first_name`, `last_name`, `address`, `email`, `user_name`, `password`, `contact_number`) VALUES
(41, 'fff', 'ffeffe', 'ffwefwe', 'fewe@gmail.com', '', '', '0212121'),
(42, 'ddd122', 'ddd', 'ddd', 'dd@gmail.com', '', '', '3333'),
(60, 'Dileeshi', 'Sathsarani', '217, Poromaruwa, Welambada.', 'ss@gmail.com', 'skjbjb', 'bk', '0716891830');

-- --------------------------------------------------------

--
-- Table structure for table `subject`
--

CREATE TABLE `subject` (
  `Id` bigint(20) NOT NULL,
  `subject_code` bigint(20) NOT NULL,
  `subject_name` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `subject`
--

INSERT INTO `subject` (`Id`, `subject_code`, `subject_name`) VALUES
(16, 23, 'strinffff'),
(17, 12, 'swsw'),
(18, 22, 'ddd'),
(19, 24, 'ssss'),
(20, 12, 'ssss'),
(21, 21, 'swsw');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20240115055345_thirtythird_migration', '6.0.0'),
('20240115073225_thirtyfouth_migration', '6.0.0'),
('20240115120124_thirtyfifth_migration', '6.0.0'),
('20240115121028_thirtysixth_migration', '6.0.0'),
('20240117094506_thirtyseventh_migration', '6.0.0'),
('20240117102242_fourtyth_migration', '6.0.0'),
('20240117105700_fourtfirst_migration', '6.0.0'),
('20240117112131_fourtsecond_migration', '6.0.0'),
('20240119064017_fourtthird_migration', '6.0.0'),
('20240119064407_fourtyfourth_migration', '6.0.0');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `logindetails`
--
ALTER TABLE `logindetails`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `relationship`
--
ALTER TABLE `relationship`
  ADD PRIMARY KEY (`StudentsId`,`SubjectsId`),
  ADD KEY `IX_Relationship_StudentId` (`StudentId`),
  ADD KEY `IX_Relationship_SubjectId1` (`SubjectId1`),
  ADD KEY `IX_Relationship_SubjectsId` (`SubjectsId`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `subject`
--
ALTER TABLE `subject`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `logindetails`
--
ALTER TABLE `logindetails`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `student`
--
ALTER TABLE `student`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT for table `subject`
--
ALTER TABLE `subject`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `relationship`
--
ALTER TABLE `relationship`
  ADD CONSTRAINT `FK_Relationship_student_StudentId` FOREIGN KEY (`StudentId`) REFERENCES `student` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Relationship_student_StudentsId` FOREIGN KEY (`StudentsId`) REFERENCES `student` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Relationship_subject_SubjectId1` FOREIGN KEY (`SubjectId1`) REFERENCES `subject` (`Id`),
  ADD CONSTRAINT `FK_Relationship_subject_SubjectsId` FOREIGN KEY (`SubjectsId`) REFERENCES `subject` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

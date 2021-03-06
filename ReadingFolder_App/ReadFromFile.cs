﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ReadingFolder_App
{
    class ReadFromFile
    {
        private String line;
        private int counter = 0;
        private List<string> temp_word_splited_list = new List<string>();
        private List<Tuple<string, int>> wordCounted_splited = new List<Tuple<string, int>>();
        private ArrayList features = new ArrayList();





        public void Read_fileread()
        {
            var temp_store = new KeyValuePair<string, int>();

            try
            {

                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"Francky_C_Cofeeshop.txt");
                

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        line.Split();

                        foreach (string w in line.Split())
                        {
                            if (!temp_word_splited_list.Contains(w))
                            {


                                temp_word_splited_list.Add(w);

                                counter = 1;

                                wordCounted_splited.Add(new Tuple<string, int>(w, counter));


                            }
                            else if (temp_word_splited_list.Contains(w))
                            {
                                for (int i = 0; i < wordCounted_splited.Count(); i++)
                                {
                                    if (wordCounted_splited[i].Item1.Equals(w))
                                    {

                                        counter = wordCounted_splited[i].Item2;
                                        counter++;
                                        wordCounted_splited.RemoveAt(i);
                                        temp_store = new KeyValuePair<string, int>(w, counter);
                                        wordCounted_splited.Add(new Tuple<string, int>(temp_store.Key, temp_store.Value));

                                        break;
                                    }
                                }

                            }



                        }
                    }


                }

                sr.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                try
                {

                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter(@"file_to_save_C.txt");

                    for (int i = 0; i < wordCounted_splited.Count(); i++)
                    {
                        sw.WriteLine(wordCounted_splited[i]);



                    }


                    //Close the file
                    sw.Close();
                    Get_Features_Value(wordCounted_splited);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("Executing finally block.");
                }

            }
        }


        public ArrayList ReadFeatures()
        {

            String line;

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"features.txt");

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    foreach (string item in line.Split(','))
                    {
                        features.Add(item);
                    }
                    break;


                }

                //close the file
                sr.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return features;

        }


        public void Get_Features_Value(List<Tuple<string, int>> counted_features)
        {
            try
            {
                List<Tuple<string, int>> content = new List<Tuple<string, int>>();

               
                

                foreach (string f in features)
                {
                    for (int i = 0; i < counted_features.Count(); i++)
                    {
                        if (counted_features[i].Item1 == f)
                        {
                            content.Add(counted_features[i]);
                        }
                    }
                }
                // if features exist in the file then insert its value
                for (int i = 0; i < features.Count; i++)
                {
                    string temp = features[i].ToString();
                    for (int j = 0; j < content.Count; j++)
                    {

                        if (features[i].ToString() == content[j].Item1)
                        {
                            features.Insert(features.IndexOf(temp), content[j].Item2);
                            features.Remove(temp);
                        }

                    }
                    // if not found the value changes to zero
                    if (features.Contains(temp))
                    {
                        features.Insert(features.IndexOf(temp), 0);
                        features.Remove(temp);
                        temp = null;
                    }

                }

             


               
                switch (CheckFile_is_empty())
                {
                    case false:
                        bool first = false;
                   
                        for (int i = 0; i< features.Count;i++)
                        {

                            if (first == false)
                            { 
                                File.AppendAllText(@"file_to_save_x.txt", Environment.NewLine);
                                i--;
                               
                            }
                            else
                                File.AppendAllText(@"file_to_save_x.txt",features[i].ToString()+",");
                            first = true;
                        }
                        features.Add("Class C");
                        int lastIndex = features.Count - 1;
                        File.AppendAllText(@"file_to_save_x.txt", features[lastIndex].ToString());

                        break;
                    default:
                        
                        foreach (int val in features)
                        {
                            File.AppendAllText(@"file_to_save_x.txt", val.ToString()+"," );
                        }
                        features.Add("Class C");
                        int lastI = features.Count - 1;
                        File.AppendAllText(@"file_to_save_x.txt", features[lastI].ToString());
                     
                        break;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public bool CheckFile_is_empty()
        {
            //MethodBase b = MethodInfo.GetCurrentMethod();

            string text = System.IO.File.ReadAllText(@"file_to_save_x.txt");

            if ( text !="")
            {
                return false;
            }
            else
                return true;
            
            
            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            //string[] lines = System.IO.File.ReadAllLines(@"Francky_A_Camping.txt");

            //// Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            //foreach (string line in lines)
            //{
            //    // Use a tab to indent each line of the file.
            //    Console.WriteLine("\t" + line);
            //    b.GetMethodBody();
            //}


        }



    }
    }

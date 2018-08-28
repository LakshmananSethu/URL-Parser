using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication6.Models;
using HtmlAgilityPack;



namespace WebApplication6.Controllers
{
    public class TestController : ApiController
    {
        Content webcontent = new Content();


        public IHttpActionResult GetParseURL(int id)
        {
            string url = "https://github.com";
            int imagecount=0;
            int count = 0;
            url = url.Substring(0, 4) != "http" ? "http://" + url : url;
            string htmlCode = "";
            List<string> urls = new List<string>();
            List<string> words = new List<string>();
            var webcontent = new Content();
            HtmlDocument doc = new HtmlDocument();

            using (WebClient client = new WebClient())
            {
                try
                {
                    htmlCode = client.DownloadString(url);
                    doc.LoadHtml(htmlCode);
                    webcontent.htmlcontent = htmlCode;

                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
                    {
                        Console.WriteLine("text=" + node.InnerText);


                        if (node.InnerText.Trim().Length > 0)

                        {

                            string[] source = node.InnerText.Trim().Split(new char[] {' ', ',','.' }, StringSplitOptions.RemoveEmptyEntries);


                           
                            foreach (string entry in source)
                            { 
                            count = count + 1;
                                //words.Add(node.InnerText);
                                words.Add(entry);
                        }
                            Dictionary<string, int> dictionary = new Dictionary<string, int>();

                            string[] stopwords = new string[] { "and", "the", "she", "for", "this", "you", "but","that","your","he","it","with","&rarr" };
                            foreach (string word in stopwords)
                            {
                                while (words.Contains(word))
                                {
                                    words.Remove(word);
                                }
                            }



                            // Loop over all over the words in our wordList...
                            foreach (string word in words)
                            {
                                // If the length of the word is at least three letters...
                                if (word.Length >= 3)
                                {
                                    //check if the dictionary already has the word.
                                    if (dictionary.ContainsKey(word))
                                    {
                                        // If we already have the word in the dictionary, increment the count of how many times it appears
                                        dictionary[word]++;
                                    }
                                    else
                                    {
                                        // new word then add it to the dictionary with an initial count of 1
                                        dictionary[word] = 1;
                                    }

                                } // End of word length check

                            } // End of loop over each word in our input

                            // Create a dictionary sorted by value (i.e. how many times a word occurs)
                            var sortedDict = (from entry in dictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

                            // Loop through the sorted dictionary and output the top 10 most frequently occurring words
                            int loopcount = 1;
                            
                            foreach (KeyValuePair<string, int> pair in sortedDict)
                            {
                                // Output the most frequently occurring words and the associated word counts
                                Console.WriteLine(count + "\t" + pair.Key + "\t" + pair.Value);
                                loopcount++;
                                webcontent.Occurance = sortedDict;

                                // Only display the top 10 words 
                                if (loopcount > 10)
                                {
                                    break;
                                }
                            }

                        }
                    }
                    webcontent.wordcount = count;
                    webcontent.words = words;

                }

                catch (Exception ex)
                {
                    return NotFound();

                }
            }


            using (WebClient client = new WebClient())
            {

                try
                {
                    var my_img_nodes = doc.DocumentNode.SelectNodes("//img[@src]");
                    htmlCode = client.DownloadString(url);
                    doc.LoadHtml(htmlCode);
                    webcontent.htmlcontent = htmlCode;


                    // Iterate through the entire list acquired (of img elements) and do whatever you want to do.
                    foreach (HtmlNode img in my_img_nodes)
                    {

                        HtmlAttribute src = img.Attributes[@"src"];
                        imagecount++;
                        urls.Add(src.Value);

                    }
                    webcontent.imagecount = imagecount;
                    webcontent.ImageURLs = urls;
                
            }

                    catch (Exception ex)
                {
                    return NotFound();

                }
            }

            return Ok(webcontent);
        }
    }
}

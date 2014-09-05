using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;

public class sqliteTest : MonoBehaviour
{
    string nameStr = "";
    string email = "";
    string path = "";

    void Start()
    {
        string appDBPath = Application.dataPath + "/test.db";
        // 创建数据库
        DbAccess db = new DbAccess(@"Data Source = " + appDBPath);

        path = appDBPath;

        // 创建表
        //db.CreateTable("testTb", new string[] { "name", "qq", "email", "blog" },
        //        new string[] { "text", "text", "text", "text" });

        // 插入数据
        //db.InsertInto("testTb", new string[] { "'流水'", "'532322345'", "'sunlaibing88@gmail.com'", "'www.522memory.com'" });
        //db.InsertInto("testTb", new string[] { "'sun1'", "'382784515'", "'88@gmail.com'", "'www.522memory.com'" });
        //db.InsertInto("testTb", new string[] { "'sun2'", "'382784515'", "'000@gmail.com'", "'www.522memory.com'" });
        //db.InsertInto("testTb", new string[] { "'sun3'", "'382784515'", "'111@gmail.com'", "'www.522memory.com'" });

        // 删除
        //db.Delete("testTb", new string[] { "email", "email" }, new string[] { "'88@gmail.com'", "'111@gmail.com'" });

        // Update 修改
        //db.ExecuteQuery("UPDATE testTb SET name = 'sun', qq='11111' where email='sunlaibing88@gmail.com'");

        // 查询遍历
        //using (SqliteDataReader sqReader = db.SelectWhere("testTb",
        //                                            new string[] { "name", "email" },
        //                                            new string[] { "qq" },
        //                                            new string[] { " = " },
        //                                            new string[] { "'382784515'" }))
        //{
        //    while (sqReader.Read())
        //    {
        //        nameStr = sqReader.GetString(sqReader.GetOrdinal("name"));
        //        email = sqReader.GetString(sqReader.GetOrdinal("email"));
        //        Debug.Log("网名: " + nameStr);
        //        Debug.Log("邮箱: " + email);
        //    }
        //}

        db.CloseSqlConnection();
    }

    void OnGUI()
    {
        if (nameStr != null)
            GUILayout.Label(nameStr);
        if (email != null)
            GUILayout.Label(email);
        if (path != null)
            GUILayout.Label(path);
    }
}

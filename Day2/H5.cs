using System;
using System.Collections.Generic;
using System.Text;

public class QueryBuilder
{
    private readonly List<string> clauses = new();

    public void AddWhereClause(string clause)
    {
        clauses.Add(clause);
    }

    public void AddWhereClause(params Action<QueryBuilder>[] builders)
    {
        foreach (Action<QueryBuilder> action in builders)
        {
            action(this);
        }
    }

    public string Build()
    {
        StringBuilder sql = new();

        sql.AppendLine("WHERE");

        int indent = 1;

        void Print(List<string> list, ref int level)
        {
            string space = new string(' ', level * 4);

            for (int i = 0; i < list.Count; i++)
            {
                sql.Append(space);

                sql.AppendLine(list[i]);

                if (i < list.Count - 1)
                {
                    sql.Append(space);

                    sql.AppendLine("OR");
                }
            }
        }

        Print(clauses, ref indent);

        return sql.ToString();
    }
}

class H5
{
    public static void main()
    {
        QueryBuilder builder = new();

        builder.AddWhereClause("Status = 'Active'");

        builder.AddWhereClause(q =>
        {
            q.AddWhereClause("Age > 18");
            q.AddWhereClause("Age < 65");
        });

        Console.WriteLine(builder.Build());
    }
}
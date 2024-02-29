using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    private List<Team> teamList = new List<Team>();
    public List<Team> TeamList { get { return teamList; } }

    public Team CreateTeam(TeamModel model)
    {
        Team team = new Team(model);

        teamList.Add(team);
        return team;
    }

    private void AddTeamToList(Team team)
    {
        teamList.Add(team);
    }
}

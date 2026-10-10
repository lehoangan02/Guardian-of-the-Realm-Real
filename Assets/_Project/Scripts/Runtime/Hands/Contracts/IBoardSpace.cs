using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public interface IBoardSpace
    {
        Vector3 WorldToBoardPoint(Vector3 worldPoint);
        Vector3 BoardToWorldPoint(Vector3 boardLocalPoint);
        Vector3 WorldToBoardDirection(Vector3 worldDirection);
        Vector3 BoardToWorldDirection(Vector3 boardLocalDirection);
    }
}

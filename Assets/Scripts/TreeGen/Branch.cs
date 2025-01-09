
using System.Collections.Generic;
using UnityEngine;

public class Branch
{
    public Vector3 start;
    public Vector3 end;
    public Vector3 direction;
    public Branch parent;
    public List<Branch> children = new List<Branch>();
	public List<Vector3> attractors = new List<Vector3>();
    public int depth = 0;
    public int verticesId;


	public Branch(Vector3 inStart, Vector3 inEnd, Vector3 inDirection, Branch inParent = null)
    {
        start = inStart;
        end = inEnd;
        direction = inDirection;
        parent = inParent;

        if (parent != null)
        {
            parent.children.Add(this);
            depth = parent.depth + 1;
        }
    }
}

//// Update is called once per frame
//void Update()
//{
//        // if at least an attraction point has been found, we want our tree to grow towards it
//        if (_activeAttractors.Count != 0)
//        {
//            // because new extremities will be set here, we clear the current ones
//            _extremities.Clear();

//            // new branches will be added here
//            List<Branch> newBranches = new List<Branch>();

//            foreach (Branch b in _branches)
//            {
//                // if the branch has attraction points, we grow towards them
//                if (b._attractors.Count > 0)
//                {
//                    // we compute the direction of the new branch
//                    Vector3 dir = new Vector3(0, 0, 0);
//                    foreach (Vector3 attr in b._attractors)
//                    {
//                        dir += (attr - b._end).normalized;
//                    }
//                    dir /= b._attractors.Count;
//                    // random growth
//                    dir += RandomGrowthVector();
//                    dir.Normalize();

//                    // our new branch grows in the correct direction
//                    Branch nb = new Branch(b._end, b._end + dir * _branchLength, dir, b);
//                    nb._distanceFromRoot = b._distanceFromRoot + 1;
//                    b._children.Add(nb);
//                    newBranches.Add(nb);
//                    _extremities.Add(nb);
//                }
//                else
//                {
//                    // if no attraction points, we only check if the branch is an extremity
//                    if (b._children.Count == 0)
//                    {
//                        _extremities.Add(b);
//                    }
//                }
//            }

//            // we merge the new branches with the previous ones
//            _branches.AddRange(newBranches);
//        }
//        else
//        {
//            // we grow the extremities of the tree
//            for (int i = 0; i < _extremities.Count; i++)
//            {
//                Branch e = _extremities[i];
//                // the new branch starts where the extremity ends
//                Vector3 start = e._end;
//                // we add randomness to the direction
//                Vector3 dir = e._direction + RandomGrowthVector();
//                // we add the direction multiplied by the branch length to get the end point
//                Vector3 end = e._end + dir * _branchLength;
//                // a new branch can be created with the same direction as its parent
//                Branch nb = new Branch(start, end, dir, e);

//                // the current extrimity has a new child
//                e._children.Add(nb);

//                // let's add the new branch to the list and set it as the new extremity 
//                _branches.Add(nb);
//                _extremities[i] = nb;
//            }
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StateParameters
{
    [Tooltip("Indicates if the action's check must be true or false")]
    public bool[] actionvalues;
    [Tooltip("Action that is gonna be executed")]
    public Action[] actions;
    [Tooltip("If the action's check equeals actionValue, next is puched")]
    public State nextStates;
    [Tooltip("All actions are executed or just one")]
    public bool and;
}

public abstract class State : ScriptableObject 
{
    public StateParameters[] stateParameters;

    protected State CheckActions(GameObject owner)
    {
        //recorre todo el array
        //devolvera el siguiente estado que toque si alguna de sus acciones se cumple, o null si es al contrario
        for(int i = 0; i < stateParameters.Length; i++)
        {
            bool AllActions = true;
            for(int j = 0; j < stateParameters[i].actions.Length; j++)
            {

                if (stateParameters[i].actions[j].Check(owner) == stateParameters[i].actionvalues[j])
                {
                    if (!stateParameters[i].and) //Si solo se tiene que cumplir una
                    {
                        //devolvemos directamente el siguiente estado
                        return stateParameters[i].nextStates;
                    }
                }
                else if (stateParameters[i].and)
                {
                    AllActions = false;
                    break;//Salimos del bucle porque una accion no se a cumplido y estamos en and
                }
                //else if (stateParameters[i].and)
                //{
                //    //Estamos seguros de que esta accion no se ha cumplido y el diseñadro me ha marcado que se tienen que cumplir todas
                //    continue;
                //}
            }

            //Si llegamos aqui, significa que el diseñador ha marcado que todas las acciones tienen que cumplirse y ademas se han cumplido
            //Si esta aqui es porque todas las acciones se han cumplido
            //Tenemos que comprobar si de verdad se han cumplido todas
            if (stateParameters[i].and && AllActions)
            {
                return stateParameters[i].nextStates;
            }

            //if (stateParameters[i].actionvalue == stateParameters[i].action.Check(owner)) //Hay que mirar si se cumple elvalor de la accion
            //{
            //    return stateParameters[i].nextStates;
            //}
        }

        return null; //ninguna accion se cumple, por lo que no cambia de estado
    }

    // Comprueba si las acciones se cumplen y ademas, ejecuta el comportamiento asociado al estado
    public abstract State Run(GameObject owner);

    public void DrawAllActionsGizmos(GameObject owner)
    {
        foreach(StateParameters parameter in stateParameters)
        {
            foreach(Action action in parameter.actions)
            {
                action.DrawGizmos(owner);
            }
        }
    }
}

const api_url = "http://localhost:10010";

export const startPlan = async () => {
    const url = `${api_url}/Plan`;
    const response = await fetch(url, {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify({}),
    });

    if (!response.ok) throw new Error("Failed to create plan");

    return await response.json();
};

export const addProcedureToPlan = async (planId, procedureId) => {
    const url = `${api_url}/Plan/AddProcedureToPlan`;
    var command = { planId: planId, procedureId: procedureId };
    const response = await fetch(url, {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify(command),
    });

    if (!response.ok) throw new Error("Failed to create plan");

    return true;
};

export const assignUsersToPlanProcedure = async (planId, procedureId, userIds=null) => {
    const url = `${api_url}/PlanProcedure/AssignUsersToPlanProcedure`;
    if(userIds != null)
    {
        var command = { planId: planId, procedureId: procedureId, userIds: userIds };
    
        const response = await fetch(url, {
              method: "POST",
              headers: {
                   Accept: "application/json",
                   "Content-Type": "application/json",
              },
              body: JSON.stringify(command),
        });

        if (!response.ok) throw new Error("Failed to Add Users to Procedure");        
    }
    return true;
};

export const getProcedures = async () => {
    const url = `${api_url}/Procedures`;
    const response = await fetch(url, {
        method: "GET",
    });

    if (!response.ok) throw new Error("Failed to get procedures");

    return await response.json();
};

export const getPlanProcedures = async (planId) => {
    const url = `${api_url}/PlanProcedure?$filter=planId eq ${planId}&$expand=procedure`;
    const response = await fetch(url, {
        method: "GET",
    });

    if (!response.ok) throw new Error("Failed to get plan procedures");

    return await response.json();
};

export const getPlanProcedures = async (planId,procedureId) => {
    const url = `${api_url}/PlanProcedure?$filter=planId eq ${planId}&procedureId eq ${procedureId}&$expand=procedure`;
    const response = await fetch(url, {
        method: "GET",
    });

    if (!response.ok) throw new Error("Failed to get plan procedures");

    return await response.json();
};

export const getUsers = async () => {
    const url = `${api_url}/Users`;
    const response = await fetch(url, {
        method: "GET",
    });

    if (!response.ok) throw new Error("Failed to get users");

    return await response.json();
};

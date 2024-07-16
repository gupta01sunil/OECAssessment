import React, { useState, useEffect } from "react";
import ReactSelect from "react-select";
import {
    assignUsersToPlanProcedure,
  } from "../../../api/api";

const PlanProcedureItem = ({ planId, procedure ,users }) => {
    const [selectedUsers, setSelectedUsers] = useState([]);
    
    const handleAssignUserToProcedure = async (e) => {
        setSelectedUsers(e);
        await assignUsersToPlanProcedure(planId, procedure.procedureId,e.selectedUsers); 
    };

    return (
        <div className="py-2">
            <div>
                {procedure.procedureTitle}
            </div>

            <ReactSelect
                className="mt-2"
                placeholder="Select User to Assign"
                isMulti={true}
                options={users}
                value={selectedUsers}
                onChange={(e) => handleAssignUserToProcedure(e)}
            />
        </div>
    );
};

export default PlanProcedureItem;

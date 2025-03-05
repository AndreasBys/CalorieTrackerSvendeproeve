// importing goal model
import MacroGoal from "../models/macroGoal.js"

// creates a new macro goal or updates if there's one for the current day
export const createMacroGoal = async (req, res) => {
    // sets the user variable to the id of the user making the request
    const userId = req.user.id
    req.body.user = userId;
    try {
        // sets startDate to current date & time
        req.body.startDate = new Date();
        req.body.startDate.setUTCHours(0, 0, 0, 0);

        // tries to find macro goal for the current day
        const updatedMacroGoal = await MacroGoal.findOneAndUpdate({ startDate: req.body.startDate, user: userId }, req.body, { new: true })

        // sends to catch if nothing is found
        if (!updatedMacroGoal) throw new Error('No goal found')

        // returns updated goal
        res.status(200).json({ msg: "goal updated", macroGoal: updatedMacroGoal })
    } catch (error) {
        try {
            // if no goal is found for current day tries to find the latest goal and update end date
            await MacroGoal.findOneAndUpdate({ user: userId },
                // sets end date to the day before the new goal
                {
                    endDate: new Date(req.body.startDate)
                        .setUTCDate(req.body.startDate.getUTCDate() - 1)
                },
            ).sort({ startDate: -1 })
        } finally {
            try {
                // saves the new goal
                const macroGoal = await new MacroGoal(req.body).save()

                // returns the created goal
                res.status(201).json({ msg: 'goal saved', macroGoal })
            } catch (error) {
                // returns status 500 if error
                res.status(500).json({ msg: 'unable to create new goal' })
            }
        }
    }
}

// gets the current goal for the user
export const getCurrentGoal = async (req, res) => {
    try{
        // finds goal through user id and sort it to the latest goal
        const macroGoal = await MacroGoal.findOne({ user: req.user.id }).sort({ startDate: -1 })

        // throws error if no goal is found
        if(!macroGoal) throw new Error('No goal found')

        // returns status 200 and the users last goal
        res.status(200).json({ macroGoal })
    } catch (error) {
        // returns status 500 error if failed or no goals found
        res.status(500).json({ msg: "unable to get goal" })
    }
};

// gets all goals between two dates
export const getGoalsBetweenDates = async (req, res) => {
    try {
        // finds all goals between two dates
        const macroGoals = await MacroGoal.find({
            user: req.user.id,
            $and: [
                { startDate: { $lte: new Date(req.query.endDate) } },
                { $or: [
                    { endDate: { $gte: new Date(req.query.startDate) } }, 
                    { endDate: { $exists: false } }] 
                }]
        })

        // if no goals are found throws error
        if (!macroGoals) throw new Error('No goals found')

        // returns status 200 and the users goals
        res.status(200).json({ macroGoals });
    }catch (error) {
        // returns status 500 error if failed or no goals found
        res.status(500).json({ msg: "unable to find any goals" })
    }
};
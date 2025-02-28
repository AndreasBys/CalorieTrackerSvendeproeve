// importing user model
import User from "../models/user.js"

// Get method - gets specific user through Id
export const getUser = async (req, res) => {
    // gets id from request params
    const id = req.params.id

    // returns 401 if the user isn't admin or user Id doesn't match
    if (!(req.user.admin || req.user.id == id))
        return res.status(401).json({ message: 'No access' });

    // finds user through id
    await User.findById(id)
        .then((user) => {
            // returns 404 if user isn't found | 200 if user is found
            if (!user) {
                return res.status(404).json({ msg: "User not found" });
            }
            res.status(200).json({ user: user })
        })
        .catch((error) => {
            // logs and returns status 500 error if failed
            console.log(error)
            res.status(500).json({ msg: "Unable to get user" })
        })
};

// Delete method - deletes a user through id
export const deleteUser = async (req, res) => {
    // gets user id from request params
    const id = req.params.id

    // returns 401 if the user isn't admin or user Id doesn't match
    if (!(req.user.admin || req.user.id == id))
        return res.status(401).json({ message: 'No access' });

    // deletes the user through id
    await User.findByIdAndDelete(id)
        .then((user) => {
            // returns 404 if user isn't found | 200 if user is found
            if (!user) {
                return res.status(404).json({ msg: "User not found" });
            }
            res.status(200).json({ msg: "Following user has been deleted", user: user })
        })
        .catch((error) => {
            // logs and returns status 500 if error 
            console.log(error)
            res.status(500).json({ msg: "Unable to get user" })
        })
}

// Update method - updates a user through id
export const updateUser = async (req, res) => {

    const id = req.params.id;

    // returns 401 if the user isn't admin or user Id doesn't match
    if (!(req.user.admin || req.user.id == id))
        return res.status(401).json({ message: 'No access' });
    await User.findById(id)
        .then(async (user) => {
            // returns 404 if user isn't found | 200 if user is found
            if (!user) {
                return res.status(404).json({ msg: "User not found" });
            }

            // updates user with request body
            Object.keys(req.body).forEach(key => {
                user[key] = req.body[key];
            });

            // runs save function to make sure password gets hashed
            const updatedUser = await user.save();
            res.status(200).json({ msg: "user updated", user: updatedUser })
        })
        .catch((error) => {
            // logs and returns status 500 if error 
            console.log(error)
            res.status(500).json({ msg: "Unable to update user" })
        });
};
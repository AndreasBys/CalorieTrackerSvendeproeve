// imports user model
import User from "../models/user.js";

// henter bruger
export const getUser = async (req, res) => {
    const id = req.params.id;

    // tjekker om brugeren har adgang til infoen
    if (!(req.user.admin || req.user.id == id)) {
        return res.status(401).json({ message: 'No access' });
    }

    try {
        // finder bruger i databasen
        const user = await User.findById(id);
        if (!user) {
            return res.status(404).json({ msg: "User not found" });
        }

        // returnerer bruger
        res.status(200).json({ user });
    } catch (error) {
        // håndterer fejl
        console.log(error);
        res.status(500).json({ msg: "Unable to get user" });
    }
};

// sletter bruger
export const deleteUser = async (req, res) => {
    const id = req.params.id;

    // tjekker om brugeren har adgang til dette
    if (!(req.user.admin || req.user.id == id)) {
        return res.status(401).json({ message: 'No access' });
    }

    try {
        // sletter bruger fra databasen
        const user = await User.findByIdAndDelete(id);
        if (!user) {
            return res.status(404).json({ msg: "User not found" });
        }
        res.status(200).json({ msg: "Following user has been deleted", user });
    } catch (error) {
        // håndterer fejl
        console.log(error);
        res.status(500).json({ msg: "Unable to delete user" });
    }
};

// opdaterer bruger
export const updateUser = async (req, res) => {
    const id = req.params.id;

    // tjekker om brugeren har adgang til dette
    if (!(req.user.admin || req.user.id == id)) {
        return res.status(401).json({ message: 'No access' });
    }

    try {
        // finder bruger i databasen
        const user = await User.findById(id);
        if (!user) {
            return res.status(404).json({ msg: "User not found" });
        }

        // filtrer req.body: Kun admin-brugere må ændre "admin" feltet
        if (!req.user.admin) {
            delete req.body.admin;  // fjerner admin-feltet fra opdateringen
        }

        // opdaterer brugerens data med de nye værdier
        Object.assign(user, req.body);

        // gemmer ændringer til databasen
        const updatedUser = await user.save();
        res.status(200).json({ msg: "User updated", user: updatedUser });
    } catch (error) {
        // håndterer fejl
        console.log(error);
        res.status(500).json({ msg: "Unable to update user" });
    }
};
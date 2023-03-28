using System;
using Cysharp.Threading.Tasks;
using Proyecto26;
using WitchCompany.Core.Runtime;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.Tool.Editor_API
{
    public static class WitchAPI
    {
     
        public static async UniTask<JAuth> Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public static async UniTask<JAuth> Refresh(JAuth auth)
        {
            throw new NotImplementedException();
        }

        public static UniTask Logout()
        {
            throw new NotImplementedException();
        }
        
        private static async UniTask<JResponse<T>> RequestWithSafeAuth<T>(RequestHelper helper)
        {
            var res = await Request<T>(helper);
            
            // // 토큰 만료일 경우,
            // if (res.statusCode == 401)
            // {
            //     var refresh = await Refresh();
            //     // 실패
            //     if (refresh == null)
            //     {
            //         if(Game.EnableAPILog)
            //             Debug.LogWarning("토큰 리프래쉬 실패");
            //         return res;
            //     }
            //     // 성공
            //     else
            //     {
            //         if(Game.EnableAPILog)
            //             Debug.Log("토큰 리프래쉬 성공!");
            //         
            //         // 토큰 저장
            //         WebHelper.WriteLocalStorage(LocalStorageKey.AuthToken, refresh.accessToken);
            //         Game.DB.authToken = refresh.accessToken;
            //         
            //         // 요청 재시도
            //         res = await RestAsync.RawRequest<Response<T>>(helper, Game.EnableAPILog);
            //     }
            // }

            return res;
        }

        private static UniTask<JResponse<T>> Request<T>(RequestHelper helper) => RestAsync.Request<JResponse<T>>(helper, true);
    }
}
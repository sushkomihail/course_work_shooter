using System.Collections;
using System.Collections.Generic;
using Extensions;
using InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WeaponSystem
{
    public class WeaponSwitcher
    {
        private readonly PlayerInputSystem _input;
        private readonly Weapon[] _weapons;
        private readonly float _switchTime;
        private readonly Vector3 _switchOffset;
        private readonly Dictionary<string, float> _keyCodes = new Dictionary<string, float>();

        private int _currentWeaponIndex;

        public bool IsSwitching { get; private set; }
        public Weapon CurrentWeapon { get; private set; }

        public WeaponSwitcher(PlayerInputSystem input, Weapon[] weapons, float switchTime, Vector3 switchOffset)
        {
            _input = input;
            _weapons = weapons;
            _switchTime = switchTime;
            _switchOffset = switchOffset;
            
            AddSwitchingBindings();
            SetStartWeapon();
        }

        public IEnumerator Switch(string key)
        {
            IsSwitching = true;
            
            CurrentWeapon.gameObject.SetActive(false);

            if (key == "y") UpdateWeaponIndexByMouseScroll();
            else _currentWeaponIndex = (int)_keyCodes[key];
            
            CurrentWeapon = _weapons[_currentWeaponIndex];
            
            CurrentWeapon.transform.localPosition += _switchOffset;
            CurrentWeapon.gameObject.SetActive(true);
            
            Vector3 currentOffset = _switchOffset;
            float elapsedTime = 0;

            while (elapsedTime < _switchTime)
            {
                float lerpFraction = elapsedTime / _switchTime;
                currentOffset = Vector3.Lerp(currentOffset, Vector3.zero, lerpFraction);
                CurrentWeapon.transform.localPosition += currentOffset;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            IsSwitching = false;
        }

        public void UpdateMouseScroll(float mouseScroll) => _keyCodes["y"] = mouseScroll;

        private void AddSwitchingBindings()
        {
            if (_weapons.IsEmpty()) return;
            
            _keyCodes.Add("y", 0);

            for (int i = 1; i <= _weapons.Length; i++)
            {
                _keyCodes.Add(i.ToString(), i - 1);
                _input.Controls.Player.SwitchWeapon.AddBinding($"<keyboard>/{i}");
            }
        }

        private void SetStartWeapon()
        {
            CurrentWeapon = _weapons[0];
            CurrentWeapon.gameObject.SetActive(true);
        }
        
        private void UpdateWeaponIndexByMouseScroll()
        {
            float mouseScroll = _keyCodes["y"];
            
            if (mouseScroll > 0) _currentWeaponIndex += 1;
            else _currentWeaponIndex -= 1;

            _currentWeaponIndex %= _weapons.Length;

            if (_currentWeaponIndex < 0) _currentWeaponIndex += _weapons.Length;
        }
    }
}